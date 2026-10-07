using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CGIERP.WinUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : MauiWinUIApplication
{
    /// <summary>
    /// Initializes the singleton application object.  This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent(); 
        AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;

        Microsoft.UI.Xaml.Application.Current.UnhandledException += App_UnhandledException;
    }

    private void App_UnhandledException(object sender, Microsoft.UI.Xaml.UnhandledExceptionEventArgs e)
    {
        e.Handled = true; // Try to prevent the app from closing immediately
        System.Diagnostics.Debug.WriteLine($"[XAML CRASH] {e.Message}");
        System.Diagnostics.Debug.WriteLine($"[XAML CRASH STACK] {e.Exception.StackTrace}");
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
