global using Mystore.Interfaces;
global using Mystore.WinUI.ViewModels;
using Mystore.WinUI.Views.Maui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mystore.WinUI.Services;

public class AppUtil : IAppUtil
{

    public AppUtil(BaseViewModelWin baseViewModelWin)
    {

        BaseVMWin = baseViewModelWin;
        this.MainMAUIWin ??= baseViewModelWin.MainMAUIWindow;

    }
    Microsoft.Maui.Controls.Window? MainMAUIWin;
    Window? MainWindowWinUI;

    public Shell GetShell()
    {
        return new MainShell(BaseVMWin)
        ;
    }
    public Microsoft.Maui.Controls.Window LoadWindow()
    {

        if (this.MainWindowWinUI is null)
        {
            //dimmerWinUI = winUIWindowMgrService.GetOrCreateUniqueWindow<DimmerWin>(BaseViewModelWin, () => new DimmerWin());

            //UiThreads.InitializeWinUIDispatcher(dimmerWinUI!.DispatcherQueue);
        }
        else
        {
            //UiThreads.InitializeWinUIDispatcher(dimmerWinUI.DispatcherQueue);
            MainMAUIWin = BaseVMWin.MainMAUIWindow;

        }

        MainMAUIWin ??= new MainMauiWindow(BaseVMWin, this);
        return MainMAUIWin;
    }
    public BaseViewModelWin BaseVMWin { get; set; }
}
