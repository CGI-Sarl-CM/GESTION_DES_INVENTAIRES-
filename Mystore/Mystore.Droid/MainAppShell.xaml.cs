using CGIERP.Views;

namespace CGIERP;

public partial class MainAppShell : Shell
{
	public MainAppShell(BaseViewModelAndroid vm)
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
    }
}