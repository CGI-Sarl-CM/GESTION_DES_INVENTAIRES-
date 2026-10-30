namespace CGIERP.Views;

public partial class HomePage : ContentPage
{
	public HomePage(BaseViewModelAndroid vm)
	{
		InitializeComponent();
		BindingContext = vm;
		MyViewModel = vm;
	}

    public BaseViewModelAndroid MyViewModel { get; }
}