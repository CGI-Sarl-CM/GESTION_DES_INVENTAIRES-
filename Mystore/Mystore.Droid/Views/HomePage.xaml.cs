using CGIERP.Views.LaboratoryViews;

namespace CGIERP.Views;

public partial class HomePage : ContentPage
{

    public HomePage()
    {
        InitializeComponent();
    }


    private async void ClassifyBtn_Clicked(object sender, TouchEventArgs e)
    {

        //var PmaxStr = this.MaxWeightTxt.Text;
        //var EchelonStr = this.Echelon.Text;
        //if (!string.IsNullOrEmpty(PmaxStr) && !string.IsNullOrEmpty(EchelonStr))
        //{
        //	float PmaxFloat = float.Parse(PmaxStr);
        //	float EchelonFloat = float.Parse(EchelonStr);

        //	MyViewModel.CurrentBalance.MaximumWeight = PmaxFloat;
        //	MyViewModel.CurrentBalance.EchelonVerif = EchelonFloat;

        //	var EchNumber = PmaxFloat / EchelonFloat;
        //	MyViewModel.CurrentBalance.EchelonNumbers = EchNumber;

        //}
    }

    private void WeightUnitRadioBtns_SelectedChanged(object sender, SelectedItemChangedEventArgs e)
    {

    }

    private void WeightUnitRadioBtns_Loaded(object sender, EventArgs e)
    {
        //WeightUnitRadioBtns.ItemsSource = new List<string>() { "mg", "g", "kg" };
    }

    private void WeightUnitRadioBtnss_Loaded(object sender, EventArgs e)
    {

        //WeightUnitRadioBtnss.ItemsSource = new List<string>() { "mg", "g", "kg" };
    }

    private void SegmentedButton_Loaded(object sender, EventArgs e)
    {
        //SegBtn.ItemsSource = new List<string>() { "mg", "g", "kg" };
    }

    private void GoToQHSEBtn_Clicked(object sender, TouchEventArgs e)
    {

    }

    private async void GoToLaboratory_Clicked(object sender, TouchEventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(LabOverView));
    }

    private void GoToLabClassifyBalance_Clicked(object sender, TouchEventArgs e)
    {

    }
}