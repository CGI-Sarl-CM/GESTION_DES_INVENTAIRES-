using Mystore.ViewModel;
using System.Diagnostics;

namespace Mystore;

public partial class MainPage : ContentPage
{
    private readonly ConnexionViewModel vm;
    int count = 0;

    public MainPage(ConnexionViewModel Vm)
    {
        InitializeComponent();
        vm = Vm;
        BindingContext = vm;

    }

}

 
