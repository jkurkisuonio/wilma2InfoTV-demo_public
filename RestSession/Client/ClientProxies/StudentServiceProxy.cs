using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using SharedLibraries.ClientUtilities;
using SharedLibraries.ShareTypes;

namespace Client.ClientProxies
{
    public static class StudentServiceProxy
    {
        private static string LoginUrl;
        private static string GetStudentsUrl;

        static StudentServiceProxy()
        {
            LoginUrl = ConfigurationManager.AppSettings["LoginUrl"];
            GetStudentsUrl = ConfigurationManager.AppSettings["GetStudentsUrl"];
        }

        public static ServiceStatus Login(AppUserCredentail credentail)
        {
            // Serialize the students to json
            var serializer = new JavaScriptSerializer();
            var jsonRequestString = serializer.Serialize(credentail);
            var bytes = Encoding.UTF8.GetBytes(jsonRequestString);

            // Initiate the HttpWebRequest with session support with CookiedFactory
            var request = CookiedRequestFactory.CreateHttpWebRequest(LoginUrl);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            // Send the json data to the Rest service
            var postStream = request.GetRequestStream();
            postStream.Write(bytes, 0, bytes.Length);
            postStream.Close();

            // Get the login status from the service
            var response = (HttpWebResponse) request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var jsonResponseString = reader.ReadToEnd();
            reader.Close();
            response.Close();

            // Deserialize the json and return the result
            return serializer.Deserialize<ServiceStatus>(jsonResponseString);
        }

        public static ServiceResult<List<Student>> GetStudentsWithCookie()
        {
            var request = CookiedRequestFactory.CreateHttpWebRequest(GetStudentsUrl);
            return GetStudents(request);
        }

        public static ServiceResult<List<Student>> GetStudentsWithoutCookie()
        {
            var request = (HttpWebRequest)WebRequest.Create(GetStudentsUrl);
            return GetStudents(request);
        }

        // private utility function
        private static ServiceResult<List<Student>> GetStudents(HttpWebRequest request)
        {
            request.Method = "GET";
            request.Accept = "application/json";

            var response = (HttpWebResponse)request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var jsonResponseString = reader.ReadToEnd();
            reader.Close();
            response.Close();

            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<ServiceResult<List<Student>>>(jsonResponseString);
        }
    }
}
