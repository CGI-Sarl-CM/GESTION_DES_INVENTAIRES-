using Mystore.Interfaces;
using Mystore.ViewModel;

namespace Mystore;

public partial class App : Application
{
    public App(IAppUtil appUtil, BaseViewModel vm)
    {
        InitializeComponent();
        AppUtilImple = appUtil;
        MyViewModel = vm;
    }
    IAppUtil AppUtilImple;
    BaseViewModel MyViewModel;

    protected override Window CreateWindow(IActivationState? activationState)
    {


        var win = AppUtilImple.LoadWindow();
        return win;
    }
}
