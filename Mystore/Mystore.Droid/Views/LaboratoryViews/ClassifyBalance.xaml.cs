namespace CGIERP.Views.LaboratoryViews;

public partial class ClassifyBalance : ContentPage
{
    public ClassifyBalance(LaboratoryViewModel vm)
    {
        InitializeComponent();
        MyViewModel = vm;
        this.BindingContext = vm;
    }
    public LaboratoryViewModel MyViewModel { get; }


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





    private void TypeOfVerification_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        var selItem = e.NewIndex;
        if (selItem is not null)
        {
            var selItemIndx = (int)selItem;
            MyViewModel.SelectedVerificationsType = MyViewModel.VerificationsTypeCollection[selItemIndx];
        }
    }

    private void MassSegControl_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        var SelItem = MyViewModel.MassUnitsCollection[(int)e.NewIndex!];

        //MyViewModel.SelectedMassUnit = SelItem;
    }

    private void ManualClassificationCmbBox_SelectedChanged(object sender, SelectedItemChangedEventArgs e)
    {
        var selItem = e.SelectedItem as Material.Components.Maui.MenuItem;
        if (selItem is null)
        {
            return;
        }
        var newClass = selItem.Text;
        MyViewModel.CurrentBalance.ClassNumber = int.Parse(newClass);
    }

    private void ClassifyBtn_LongPressed(object sender, TouchEventArgs e)
    {
        ManualClassificationPopup.Show();
    }

    private void ClassifyBtn_RightClicked(object sender, TouchEventArgs e)
    {
        ManualClassificationPopup.Show();

    }

    private void MassSegControlMaxWeight_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        var SelItem = MyViewModel.MassUnitsCollection[(int)e.NewIndex!];

        MyViewModel.SelectedPMaxUnit = SelItem;
    }

    private void MassSegControlEchelon_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        var SelItem = MyViewModel.MassUnitsCollection[(int)e.NewIndex!];

        MyViewModel.SelectedEchelonUnit = SelItem;
    }
}