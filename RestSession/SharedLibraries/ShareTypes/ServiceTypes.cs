using System;

namespace SharedLibraries.ShareTypes
{
    // Client use this class to send the user credential
    // to login to the service
    public class AppUserCredentail
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    // Service use this class to send information to the
    // client, if the client is logged in.
    public class Student
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentTime { get; set; }
        public int Score { get; set; }
    }
}
