using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Browser;
using Moneyero.Views;

namespace Moneyero
{
    /// <summary>
    /// Represents the main application.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Creates a new <see cref="App"/> instance.
        /// </summary>
        public App()
        {
            Startup += OnApplicationStartup;
            UnhandledException += OnApplicationUnhandledException;

            InitializeComponent();
        }

        private void OnApplicationStartup(object sender, StartupEventArgs e)
        {
            RootVisual = new MoneyeroView();
        }

        private static void OnApplicationUnhandledException(
            object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!Debugger.IsAttached)
            {
                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(() => ReportErrorToDom(e));
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        private static void ReportErrorToDom(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}