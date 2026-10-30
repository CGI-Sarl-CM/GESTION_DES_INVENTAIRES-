namespace CGIERP.Views.LaboratoryViews;

public partial class LabOverView : ContentPage
{

    public LabOverView(LaboratoryViewModel vm)
    {
        InitializeComponent();
        MyViewModel = vm;
        this.BindingContext = vm;
    }
    public LaboratoryViewModel MyViewModel { get; }

    private async void Button_Clicked(object sender, TouchEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(ClassifyBalance));
    }
}