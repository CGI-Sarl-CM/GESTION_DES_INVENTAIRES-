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
        
    }


    public Shell GetShell()
    {
        return new MainAppShell(BaseVMAnd);
    }
    public Microsoft.Maui.Controls.Window LoadWindow()
    {
        Window window = new Window();
        window.Page = GetShell();
        return window;
    }

    public BaseViewModelAndroid BaseVMAnd { get; }
}
