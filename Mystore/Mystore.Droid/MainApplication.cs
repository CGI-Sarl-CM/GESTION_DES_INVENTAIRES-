using Android.App;
using Android.Runtime;
using System.Diagnostics;

namespace CGIERP.Droid
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {


           
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

            
            
            
        }
       

        private void OnUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            Debug.WriteLine($"UNOBSERVED TASK EXCEPTION: {e.Exception}");

            e.SetObserved(); // Prevent app from crashing due to unobserved exception
        }
        private void CurrentDomain_UnhandledException(object sender, System.UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            if (ex.Message.Contains("Operation is not valid due to the current state of the object.")
                && ex.StackTrace?.Contains("WinRT.ExceptionHelpers") == true)
            {
                // This is the noisy exception we want to ignore.
                // Just return and don't log it.
                return;
            }
            if (ex.Message.Contains("Unable to read data from the transport connection: An existing connection was forcibly closed by the remote host..\r\n"))
            {
                // This is the noisy exception we want to ignore.
                // Just return and don't log it.
                return;
            }
            if (ex.Message.Contains("No such host is known."))
            {
                // This is the noisy exception we want to ignore.
                // Just return and don't log it.
                return;
            }
            if (ex.Message.Contains("Exception has been thrown by the target of an invocation"))
            {
                // This is the noisy exception we want to ignore.
                // Just return and don't log it.
                return;
            }
            Exception exx = (Exception)e.ExceptionObject;

            string errorDetails = $"********** UNHANDLED EXCEPTION! Winui **********\n" +
                                     $"Exception Type: {exx.GetType()}\n" +
                                     $"Message: {exx.Message}\n" +
                                     $"Source: {exx.Source}\n" +
                                     $"Stack Trace: {exx.StackTrace}\n";

            // ... Log to file, etc.
            Debug.WriteLine(errorDetails);
        }

        private static void CurrentDomain_FirstChanceException(object? sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
        {
            string errorDetails = $"********** UNHANDLED EXCEPTION! **********\n" +
                                     $"Exception Type: {e.Exception.GetType()}\n" +
                                     $"ChatMessage: {e.Exception.Message}\n" +
                                     $"Source: {e.Exception.Source}\n" +
                                     $"Stack Trace: {e.Exception.StackTrace}\n";

            if (e.Exception.InnerException != null)
            {
                errorDetails += "***** Inner Exception *****\n" +
                                $"ChatMessage: {e.Exception.InnerException.Message}\n" +
                                $"Stack Trace: {e.Exception.InnerException.StackTrace}\n";
            }

            // Print to Debug Console
            Debug.WriteLine(errorDetails);


        }
        private static readonly object _logLock = new();

        private static void CurrentDomain_ProcessExit(object? sender, EventArgs e)
        {

        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();

    }
}
