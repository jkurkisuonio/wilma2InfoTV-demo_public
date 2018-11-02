using System;
using System.Windows;
using Client.ViewModels.BaseModel;
using SharedLibraries.ShareTypes;
using System.Collections.Generic;
using Client.ClientProxies;

namespace Client.ViewModels
{
    class MainWindowVM : ViewModelBase
    {
        // Properties & commands for login
        private Visibility loginVisibility;
        public Visibility LoginVisibility
        {
            get { return loginVisibility; }
            private set { loginVisibility = value; NotifyPropertyChanged("LoginVisibility"); }
        }

        private AppUserCredentail userCredentail;
        public AppUserCredentail UserCredentail
        {
            get { return userCredentail; }
            set { userCredentail = value; NotifyPropertyChanged("UserCredentail"); }
        }

        public RelayCommand LoginCommand { get; private set; }
        private void Login()
        {
            if (string.IsNullOrWhiteSpace(UserCredentail.UserName))
            {
                ShowMessage("Please type in a user name");
                return;
            }

            if (string.IsNullOrWhiteSpace(UserCredentail.Password))
            {
                ShowMessage("Please type in a password");
                return;
            }

            ServiceStatus status;
            try
            {
                status = StudentServiceProxy.Login(UserCredentail);
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message);
                return;
            }

            if (!status.Success)
            {
                ShowMessage(status.Message);
                return;
            }

            LoginVisibility = Visibility.Collapsed;
        }

        public RelayCommand GetStudentsNoCookieCommand { get; private set; }
        private void GetStudentsNoCookie()
        {
            Students = null;
            ServiceResult<List<Student>> result;
            try
            {
                result = StudentServiceProxy.GetStudentsWithoutCookie();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                return;
            }

            if (! result.Status.Success)
            {
                ShowMessage(result.Status.Message);
                return;
            }

            Students = result.Result;
        }

        public RelayCommand GetStudentWithCookieCommand { get; private set; }
        private void GetStudentWithCookie()
        {
            Students = null;
            ServiceResult<List<Student>> result;
            try
            {
                result = StudentServiceProxy.GetStudentsWithCookie();
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
                return;
            }

            if (!result.Status.Success)
            {
                ShowMessage(result.Status.Message);
                return;
            }

            Students = result.Result;
        }

        // The list of students received from the service,
        // The list will be bound to the view
        private List<Student> students;
        public List<Student> Students
        {
            get { return students; }
            private set
            {
                students = value;
                NotifyPropertyChanged("Students");
            }
        }


        // Wire all the Commands
        private void WireCommands()
        {
            LoginCommand = new RelayCommand(Login);
            LoginCommand.IsEnabled = true;

            GetStudentsNoCookieCommand = new RelayCommand(GetStudentsNoCookie);
            GetStudentsNoCookieCommand.IsEnabled = true;

            GetStudentWithCookieCommand = new RelayCommand(GetStudentWithCookie);
            GetStudentWithCookieCommand.IsEnabled = true;
        }

        public MainWindowVM()
        {
            // Initialize the properties
            UserCredentail = new AppUserCredentail();

            LoginVisibility = Visibility.Visible;
            WireCommands();
        }
    }
}
