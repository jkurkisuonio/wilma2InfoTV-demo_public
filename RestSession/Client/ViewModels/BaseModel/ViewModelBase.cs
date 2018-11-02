using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Input;

namespace Client.ViewModels.BaseModel
{
    public abstract class ViewModelBase
       : DependencyObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Message dialog related properties and methods
        protected void ShowMessage(string message)
        {
            Message = message;
            MessageVisibility = Visibility.Visible;
        }

        private string message;
        public string Message
        {
            get { return message; }
            private set
            {
                if (message != value)
                {
                    message = value;
                    NotifyPropertyChanged("Message");
                }
            }
        }

        private Visibility messageVisibility;
        public Visibility MessageVisibility
        {
            get { return messageVisibility; }
            set
            {
                if (messageVisibility != value)
                {
                    messageVisibility = value;
                    NotifyPropertyChanged("MessageVisibility");
                }
            }
        }

        public RelayCommand HideMessageCommand { get; private set; }
        protected void HideMessage()
        {
            Message = null;
            MessageVisibility = Visibility.Collapsed;
        }

        protected ViewModelBase()
        {
            MessageVisibility = Visibility.Collapsed;
            HideMessageCommand = new RelayCommand(HideMessage);
            HideMessageCommand.IsEnabled = true;
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action handler;
        private bool isEnabled;

        public RelayCommand(Action handler)
        {
            this.handler = handler;
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                if (value != isEnabled)
                {
                    isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public event EventHandler CanExecuteChanged;
        public void Execute(object parameter)
        {
            handler();
        }
    }
}
