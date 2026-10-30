global using CGIERP.Interfaces;
global using CGIERP.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGIERP.Services;

public class AppUtil : IAppUtil
{

    public AppUtil(BaseViewModelAndroid baseViewModelWin)
    {

        BaseVMAnd = baseViewModelWin;
        //this.MainMAUIWin ??= baseViewModelWin.MainMAUIWindow;

    }
    Microsoft.Maui.Controls.Window? MainMAUIWin;
    Window? MainWindowWinUI;

    public Shell GetShell()
    {
        return new MainAppShell(BaseVMAnd);
    }
    public Microsoft.Maui.Controls.Window LoadWindow()
    {

        //if (this.MainWindowWinUI is null)
        //{
        //    //dimmerWinUI = winUIWindowMgrService.GetOrCreateUniqueWindow<DimmerWin>(BaseViewModelWin, () => new DimmerWin());

        //    //UiThreads.InitializeWinUIDispatcher(dimmerWinUI!.DispatcherQueue);
        //}
        //else
        //{
        //    //UiThreads.InitializeWinUIDispatcher(dimmerWinUI.DispatcherQueue);
        //    MainMAUIWin = BaseVMWin.MainMAUIWindow;

        //}

        //MainMAUIWin ??= new MainMauiWindow(BaseVMWin, this);
        //return MainMAUIWin;
    }
    public BaseViewModelWi?n BaseVMWin { get; set; }
    public BaseViewModelAndroid BaseVMAnd { get; }
}
