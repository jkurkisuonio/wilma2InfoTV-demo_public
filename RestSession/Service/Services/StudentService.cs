using System;
using System.Collections.Generic;
using System.Web;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using SharedLibraries.ShareTypes;

namespace Service.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode
        = AspNetCompatibilityRequirementsMode.Allowed)]
    public class StudentService
    {
        private readonly HttpContext context;
        public StudentService()
        {
            context = HttpContext.Current;
        }

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "StudentService/Login")]
        public ServiceStatus Login(AppUserCredentail credentail)
        {
            // Initiate status as fail to login.
            var status = new ServiceStatus() 
                { Success = false, Message = "Wrong user name and/or password" };

            // For simplicity, this example application has only one user.
            if ((credentail.UserName == "user") && (credentail.Password == "password"))
            {
                status.Success = true;
                status.Message = "Login success";
            }

            // Keep the login status in the HttpSessionState
            context.Session["USERLOGGEDIN"] = status.Success? "YES": null;

            return status;
        }


        [OperationContract]
        [WebGet(UriTemplate = "StudentService/GetStudents")]
        public ServiceResult<List<Student>> GetStudents()
        {
            var result = new ServiceResult<List<Student>>();

            // Check if client is logged in, if fail, return the status
            if ((string) context.Session["USERLOGGEDIN"] != "YES")
            {
                result.Status.Success = false;
                result.Status.Message = "Not logged in or session is over";
                return result;
            }

            // Client is logged in, create a random student list and send back
            var students = new List<Student>();
            var rand = new Random();
            for (int i = 1; i <= 20; i++)
            {
                var student = new Student();
                student.Id = i;
                student.LastName = "LName - " + i.ToString();
                student.FirstName = "FName - " + i.ToString();
                student.EnrollmentTime = DateTime.Now.AddYears(-4);
                student.Score = 60 + (int)(rand.NextDouble() * 40);

                students.Add(student);
            }

            result.Result = students;
            result.Status.Success = true;
            result.Status.Message = "Success";

            return result;
        }
    }
}