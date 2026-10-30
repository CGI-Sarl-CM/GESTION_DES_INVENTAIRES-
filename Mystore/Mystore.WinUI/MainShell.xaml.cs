using CGIERP.WinUI.Views.Maui;
using CGIERP.WinUI.Views.Maui.LaboratoryViews;

namespace CGIERP.WinUI;

public partial class MainShell : Shell
{
	public MainShell(BaseViewModelWin vm)
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(LabOverView), typeof(LabOverView));
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(ClassifyBalance), typeof(ClassifyBalance));
	}
}