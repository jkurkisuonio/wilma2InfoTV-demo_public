using System;
using System.Windows;

namespace Client
{
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException +=
                         new UnhandledExceptionEventHandler(DomainUnhandledException);
        }

        private static void DomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            MessageBox.Show(ex.Message, "Application Error",
                            MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
