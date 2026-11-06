using CGIERP.Views;
using CGIERP.Views.LaboratoryViews;

namespace CGIERP;

public partial class MainAppShell : Shell
{
	public MainAppShell(BaseViewModelAndroid vm)
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(LabOverView), typeof(LabOverView));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(ClassifyBalance), typeof(ClassifyBalance));
    }
}
