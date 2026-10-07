
global using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;

using Windows.Graphics;

using Application = Microsoft.Maui.Controls.Application;
using Compositor = Microsoft.UI.Composition.Compositor;
using FlyoutBase = Microsoft.UI.Xaml.Controls.Primitives.FlyoutBase;

using ImageSource = Microsoft.Maui.Controls.ImageSource;
namespace CGIERP.WinUI.Utilities;

using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml.Hosting;
using Microsoft.UI.Xaml.Media;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Windows.AppNotifications;
using Microsoft.Windows.AppNotifications.Builder;
using CGIERP.WinUI.Views.Winui;
using System.Runtime.InteropServices;
using Windows.Graphics;
using WinRT.Interop;
using Application = Microsoft.Maui.Controls.Application;
using Compositor = Microsoft.UI.Composition.Compositor;
using FlyoutBase = Microsoft.UI.Xaml.Controls.Primitives.FlyoutBase;
using ImageSource = Microsoft.Maui.Controls.ImageSource;



public static class PlatUtils
{
    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect { public int Left, Top, Right, Bottom; }



    public static IntPtr DimmerHandle { get; set; }
    public static bool IsAppInForeground { get; set; }
    public static AppWindowPresenter? AppWinPresenter { get; set; }
    public static OverlappedPresenter? OverLappedPres { get; set; }

    public static class ShowCloseConfirmationPopUp
    {
        const bool showCloseConfirmation = false;
        public static bool ShowCloseConfirmation
        {
            get => Preferences.Default.Get(nameof(ShowCloseConfirmation), showCloseConfirmation);
            set => Preferences.Default.Set(nameof(ShowCloseConfirmation), value);
        }
        public static void ToggleCloseConfirmation(bool showClose)
        {
            ShowCloseConfirmation = showClose;
        }
        public static bool GetCloseConfirmation()
        {
            return ShowCloseConfirmation;
        }
    }
    // Method to set the window on top
    public static void ToggleWindowAlwaysOnTop(bool topMost, AppWindowPresenter? appPresenter)
    {
        try
        {

            OverLappedPres = appPresenter as OverlappedPresenter;
            if (topMost)
            {
                OverLappedPres!.IsAlwaysOnTop = true;
            }
            else
            {
                OverLappedPres!.IsAlwaysOnTop = false;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex.Message}");
        }
    }




    public static void MiniMimizeWindow(Microsoft.Maui.Controls.Window win)
    {

        var nativeWindow = win.Handler.PlatformView;
        IntPtr windowHandle = WindowNative.GetWindowHandle(nativeWindow);
        ShowWindow(windowHandle, SW_HIDE);
        //System.Windows.SystemCommands.MinimizeWindow(win);
    }
    public static void ToggleFullScreenMode(bool IsToFullScreen, AppWindowPresenter? appPresenter)
    {
        try
        {
            OverLappedPres = appPresenter as OverlappedPresenter;
            if (IsToFullScreen)
            {
                OverLappedPres!.IsAlwaysOnTop = true;
                OverLappedPres.SetBorderAndTitleBar(false, false);
                OverLappedPres!.Maximize();

            }
            else
            {
                OverLappedPres!.IsAlwaysOnTop = false;
                OverLappedPres.SetBorderAndTitleBar(true, true);
                OverLappedPres!.Restore();
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{ex.Message}");
        }
    }

    public static void OpenAndSetWindowToEdgePosition(Microsoft.Maui.Controls.Window concernedWindow, RectInt32 positionToSet)
    {
        var nativeWindow = GetNativeWindowFromMAUIWindow(concernedWindow);
        if (nativeWindow is null) return;
        nativeWindow.AppWindow.IsShownInSwitchers = false;
        var sysBackDrop = new MicaBackdrop();
        nativeWindow.SystemBackdrop = sysBackDrop;
        var pres = nativeWindow.AppWindow.Presenter;
        //window.SetTitleBar()
        var p = pres as OverlappedPresenter;
        if (p != null)
        {
            p.SetBorderAndTitleBar(false, false);
            p.IsResizable = false;
            p.IsAlwaysOnTop = true;

        }


        nativeWindow.Activate();
        nativeWindow.AppWindow.MoveAndResize(positionToSet);

        nativeWindow.AppWindow.MoveInZOrderAtTop();

    }

    public static void ResizeWindow(this Microsoft.Maui.Controls.Window concernedWindow, SizeInt32 sizeToSet)
    {
        var nativeWindow = GetNativeWindowFromMAUIWindow(concernedWindow);
        var newRect = new RectInt32
        {

            Width = sizeToSet.Width,
            Height = sizeToSet.Height
        };
        nativeWindow.AppWindow.Resize(sizeToSet);
        nativeWindow.AppWindow.MoveInZOrderAtTop();
    }

    public static void ResizeNativeWindow(Microsoft.UI.Xaml.Window concernedWindow, SizeInt32 sizeToSet)
    {

        var newRect = new RectInt32
        {

            Width = sizeToSet.Width,
            Height = sizeToSet.Height
        };
        concernedWindow.AppWindow.Resize(sizeToSet);
        concernedWindow.AppWindow.MoveInZOrderAtTop();
    }

    public static void MoveAndResizeCenter(Microsoft.UI.Xaml.Window nativeWindow, SizeInt32 sizeToSet)
    {

        var width = DisplayArea.Primary.WorkArea.Width;
        var height = DisplayArea.Primary.WorkArea.Height;
        var newRect = new RectInt32
        {
            Height = sizeToSet.Height,
            Width = sizeToSet.Width,
            X = (width - sizeToSet.Width) / 2,
            Y = (height - sizeToSet.Height) / 2
        };
        nativeWindow.AppWindow.MoveAndResize(newRect);
        nativeWindow.AppWindow.MoveInZOrderAtTop();
    }
    public static void MoveAndResizeWindow(this Microsoft.Maui.Controls.Window concernedWindow, RectInt32 positionToSet)
    {
        var nativeWindow = GetNativeWindowFromMAUIWindow(concernedWindow);

        //var disInfo= DisplayInformation.GetForCurrentView();

        var width = DisplayArea.Primary.WorkArea.Width;
        var height = DisplayArea.Primary.WorkArea.Height;

        //var width = DisplayArea.Primary.WorkArea.Width;
        //var height = DisplayArea.Primary.WorkArea.Height;
        //var x = DisplayArea.Primary.WorkArea.X;
        //var y = DisplayArea.Primary.WorkArea.Y;
        //AppWindow.MoveAndResize(new Windows.Graphics.RectInt32
        //{
        //    Height = height,
        //    Width = 340,
        //    X = x,
        //    Y = y
        //});

        // move to left x - (width - 400)
        // move to right x + (width - 400)

        //move to top y - (height - 400)
        //move to top y + (height - 400)

        nativeWindow.AppWindow.MoveAndResize(positionToSet);

        nativeWindow.AppWindow.MoveInZOrderAtTop();
    }


    // Helper to retrieve a valid window handle from your main window
    public static IntPtr GetWindowHandle()
    {
        var window = IPlatformApplication.Current!.Services.GetService<MainWindowWinUI>()!;


        DimmerHandle = WindowNative.GetWindowHandle(window);
        return DimmerHandle;
    }

    public static async Task EnsureWindowReadyAsync(Microsoft.Maui.Controls.Window? MauiWindow = null)
    {
        if (MauiWindow == null)
        {       // Ensure there’s at least one window created by MAUI
            MauiWindow = Application.Current?.Windows.FirstOrDefault();
        }
        int attempts = 0;
        while ((MauiWindow?.Handler == null) && attempts++ < 20)
        {
            await Task.Delay(100);
            MauiWindow = Application.Current?.Windows.FirstOrDefault();
        }

        if (MauiWindow?.Handler == null)
            throw new InvalidOperationException("Window handler was not ready after waiting.");
    }

    public static Compositor GetCompositor(Microsoft.Maui.Controls.Window? MauiWindow = null)
    {
        var nativeWindow = GetNativeWindowFromMAUIWindow(MauiWindow);

        return nativeWindow.Compositor;
    }

    public static Compositor MainWindowCompositor => GetCompositor();
    public static Microsoft.UI.Xaml.Window GetNativeWindowFromMAUIWindow(Microsoft.Maui.Controls.Window? MauiWindow = null)
    {
        if (MauiWindow == null)
        {       // Ensure there’s at least one window created by MAUI
            MauiWindow = Application.Current?.Windows.FirstOrDefault();
        }
        if (MauiWindow == null)
            throw new InvalidOperationException("No MAUI window available yet.");



        var nativeWindow = MauiWindow.Handler?.PlatformView as Microsoft.UI.Xaml.Window;
        if (nativeWindow == null)
            throw new InvalidOperationException("Unable to retrieve native window.");

        return nativeWindow;
    }

    public static Microsoft.UI.Xaml.Controls.Frame? GetNativeFrame(this IElement element)
    {
        if (element.Handler?.PlatformView is Microsoft.UI.Xaml.FrameworkElement fe)
        {
            Microsoft.UI.Xaml.DependencyObject? parent = fe;
            while (parent != null)
            {
                if (parent is Microsoft.UI.Xaml.Controls.Frame frame)
                    return frame;

                parent = VisualTreeHelper.GetParent(parent);
            }
        }
        return null;
    }
    public static nint GetHWIdnInt(Microsoft.UI.Xaml.Window win)
    {
        var hwnd = WindowNative.GetWindowHandle(win);
        return hwnd;
    }
    public static string GetHWId(Microsoft.UI.Xaml.Window win)

    {
        var hwnd = WindowNative.GetWindowHandle(win);
        return hwnd.ToInt64().ToString();
    }

    public static AppWindow GetAppWindow(Microsoft.UI.Xaml.Window win)
    {
        var hwnd = WindowNative.GetWindowHandle(win);
        var id = Win32Interop.GetWindowIdFromWindow(hwnd);
        return AppWindow.GetFromWindowId(id);
    }
    public static IntPtr GetAnyWindowHandle(Microsoft.Maui.Controls.Window window)
    {

        if (window == null)
            throw new ArgumentNullException(nameof(window));
        // Get the underlying native window (WinUI).
        var nativeWindow = window.Handler?.PlatformView as Microsoft.UI.Xaml.Window ?? throw new InvalidOperationException("Unable to retrieve the native window.");

        var intPtrHandle = WindowNative.GetWindowHandle(nativeWindow);

        return intPtrHandle;
    }

    [DllImport("user32.dll")]
#pragma warning disable S4200 // Native methods should be wrapped
#pragma warning disable SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
#pragma warning disable CA1401 // P/Invokes should not be visible
    public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
#pragma warning restore CA1401 // P/Invokes should not be visible
#pragma warning restore SYSLIB1054 // Use 'LibraryImportAttribute' instead of 'DllImportAttribute' to generate P/Invoke marshalling code at compile time
#pragma warning restore S4200 // Native methods should be wrapped

    public const int SW_HIDE = 0;
    public const int SW_RESTORE = 9;
    public static class ApplicationProps
    {
        public static DisplayArea? DisplayArea { get; set; }



        public async static Task LaunchNotificationWindowAndFadeItAwayAfterSixSeconds(BaseViewModelWin vm)
        {

            //SongNotifierWindow newNotif = new SongNotifierWindow(vm);

            //newNotif.Height = 300;
            //newNotif.Width = AppUtils.UserScreenWidth;

            //Application.Current?.OpenWindow(newNotif);


            //await Task.Delay(6000);


            //Application.Current?.CloseWindow(newNotif);


        }

    }

    public static async void ClearNotifications()
    {
        _ = Task.Run(async () => await AppNotificationManager.Default.RemoveAllAsync());
    }
    public static void ShowContextMenu(this Element element)
    {
        // Get the MenuFlyout attached to the MAUI element
        var menuFlyout = Microsoft.Maui.Controls.FlyoutBase.GetContextFlyout(element);
        if (menuFlyout == null)
        {
            // No context menu to show
            return;
        }

        // Use platform-specific code to trigger the flyout
        var platformView = element.Handler?.PlatformView as Microsoft.UI.Xaml.FrameworkElement;
        if (platformView != null)
        {
            var flyoutMenu = FlyoutBase.GetAttachedFlyout(platformView);
            if (flyoutMenu is null) return;

            // The native way to show a context flyout on WinUI
            FlyoutBase.ShowAttachedFlyout(platformView);
        }
    }
    static void AnimateHoverUIElement(Microsoft.UI.Xaml.UIElement element, bool isHover, Microsoft.UI.Composition.Compositor _compositor)
    {

        var visual = ElementCompositionPreview.GetElementVisual(element);

        // scale up / down
        var scaleAnim = _compositor.CreateVector3KeyFrameAnimation();
        scaleAnim.Duration = TimeSpan.FromMilliseconds(200);
        scaleAnim.InsertKeyFrame(1f, isHover
            ? new System.Numerics.Vector3(1.05f)
            : new System.Numerics.Vector3(1f));

        // keep scale centered
        visual.CenterPoint = new System.Numerics.Vector3(
            (float)element.RenderSize.Width / 2,
            (float)element.RenderSize.Height / 2,
            0);

        visual.StartAnimation(nameof(visual.Scale), scaleAnim);


        var opacityAnim = _compositor.CreateScalarKeyFrameAnimation();
        opacityAnim.Duration = TimeSpan.FromMilliseconds(250);
        opacityAnim.InsertKeyFrame(1f, isHover ? 1f : 0.85f);   // not 0
        visual.StartAnimation(nameof(visual.Opacity), opacityAnim);


    }
   

}
