using CGIERPData;
using CGIERPData.ModelViews.LaboratoryDepartment;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.Input;
using DynamicData;
using Realms.Exceptions;
using Syncfusion.Maui.Toolkit.Popup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CGIERP.ViewModel;

public partial class LaboratoryViewModel : BaseViewModel
{
    [ObservableProperty]
    public partial BalanceClass CurrentBalance{ get; set; }

    [ObservableProperty]
    public partial ObservableCollection<string> MassUnitsCollection { get; set; }

    [ObservableProperty]
    public partial string SelectedPMaxUnit { get; set; }
    [ObservableProperty]
    public partial string SelectedEchelonUnit { get; set; }

    [ObservableProperty]
    public partial string MaximumWeight { get; set; }

    [ObservableProperty]
    public partial string EchelonVerif { get; set; } 

    [ObservableProperty]
    public partial decimal MassValue { get; set; } 



    [ObservableProperty]
    public partial ObservableCollection<string> VerificationsTypeCollection { get; set; }

    [ObservableProperty]
    public partial string SelectedVerificationsType { get; set; }

    public LaboratoryViewModel(IRealmFactory realmFactory) : base(realmFactory)
    {
        CurrentBalance = new();
        VerificationsTypeCollection = new();
        MassUnitsCollection = new();
        VerificationsTypeCollection.AddRange(new List<string> { "Verification Primitive", "Verification Periodique"});
        MassUnitsCollection.AddRange(new List<string> { "mg", "g", "kg"});
        SelectedEchelonUnit = MassUnitsCollection[1];
        SelectedPMaxUnit = MassUnitsCollection[1];
        EchelonVerif = string.Empty;
        MaximumWeight = string.Empty;
        SelectedVerificationsType = VerificationsTypeCollection.First();
    }


    [RelayCommand]
    private async Task DetermineNumberOfEchelons()
    {
        try
        {
         
            decimal maxWeightInG = decimal.Parse(MaximumWeight);
            decimal echVerifInG = decimal.Parse(EchelonVerif);

            maxWeightInG = SelectedPMaxUnit.ToLower() switch
            {
                "mg" => maxWeightInG / 1000m,
                "kg" => maxWeightInG * 1000m,
                _ => maxWeightInG
            };

            echVerifInG = SelectedEchelonUnit.ToLower() switch
            {
                "mg" => echVerifInG / 1000m,
                "kg" => echVerifInG * 1000m,
                _ => echVerifInG
            };

            decimal numberOfEchelons = maxWeightInG / echVerifInG;
            AutoClassifyBalance(numberOfEchelons, maxWeightInG, echVerifInG);
            CalculateMinWeight(CurrentBalance.ClassNumber, echVerifInG, numberOfEchelons);
            CalculerEMT(MassValue, echVerifInG);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Erreur", ex.Message, "OK");
        }
    }

    private void CalculerEMT(decimal massValue, decimal echVerifInG)
    {
        switch (SelectedVerificationsType)
        {

            case "Verification Primitive":
                switch (CurrentBalance.ClassNumber)
                {
                    case 1:
                        if ((0 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 50000))
                        {
                            CurrentBalance.EMT = (decimal)0.5 * echVerifInG;
                        }
                        if ((50000 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 200000))
                        {
                            CurrentBalance.EMT = (decimal)1.0 * echVerifInG;

                        }
                        if ((0 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 50000))
                        {
                            CurrentBalance.EMT = (decimal)1.5 * echVerifInG;

                        }
                        break;
                        
                    case 2:
                        if ((0 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 5000))
                        {
                            CurrentBalance.EMT = (decimal)0.5 * echVerifInG;
                        }
                        if ((5000 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 20000))
                        {
                            CurrentBalance.EMT = (decimal)1.0 * echVerifInG;

                        }
                        if ((20000 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 100000))
                        {
                            CurrentBalance.EMT = (decimal)1.5 * echVerifInG;

                        }
                        break;
                        
                    case 3:
                        if ((0 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 50000))
                        {
                            CurrentBalance.EMT = (decimal)0.5 * echVerifInG;
                        }
                        if ((50000 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 200000))
                        {
                            CurrentBalance.EMT = (decimal)1.0 * echVerifInG;

                        }
                        if ((0 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 50000))
                        {
                            CurrentBalance.EMT = (decimal)1.5 * echVerifInG;

                        }
                        break;
                        
                    case 4:
                        if ((0 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 50000))
                        {
                            CurrentBalance.EMT = (decimal)0.5 * echVerifInG;
                        }
                        if ((50000 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 200000))
                        {
                            CurrentBalance.EMT = (decimal)1.0 * echVerifInG;

                        }
                        if ((0 <= (massValue / echVerifInG)) && ((massValue / echVerifInG) <= 50000))
                        {
                            CurrentBalance.EMT = (decimal)1.5 * echVerifInG;

                        }
                        break;

                    default:
                        break;
                }
               
                break;

            case "Verification Periodique":
                break;
            default:
                break;
        }
    }

    private void CalculateMinWeight(int classNumber, decimal echVerifInG, decimal numbOfEchelons )
    {
        switch (CurrentBalance.ClassNumber)
        {
            case 1:
                CurrentBalance.MinimumWeight = 100 * echVerifInG;
                break;
            case 2:
                if ((echVerifInG is >= (decimal)0.001 and <= (decimal)0.05) && (100 <= numbOfEchelons) && (numbOfEchelons <= 100000))
                {
                    CurrentBalance.MinimumWeight = 20 * echVerifInG;
                }
                else
                {
                    CurrentBalance.MinimumWeight = 50 * echVerifInG;
                }

                
                break;
            case 3:
                CurrentBalance.MinimumWeight = 20 * echVerifInG;

                break;
            case 4:
                CurrentBalance.MinimumWeight = 10 * echVerifInG;

                break;
            default:
                break;
        }
    }

    private void AutoClassifyBalance(decimal numberOfEch, decimal maxWeightInG, decimal echVerifInG)
    {
        CurrentBalance.ClassNumber = (echVerifInG, numberOfEch) switch
        {
            // Class 1 (I) - We lowered the bound to >= 0.0001m (0.1mg) so it captures the lab tech's inputs!
            ( >= 0.0001m, >= 50000) => 1,

            // Class 2 (II) 
            ( >= 0.001m and <= 0.05m, >= 100 and <= 100000) => 2,
            ( >= 0.1m, >= 5000 and <= 100000) => 2,

            // Class 3 (III) 
            ( >= 0.1m and <= 2m, >= 100 and <= 10000) => 3,
            ( >= 5m, >= 500 and <= 10000) => 3,

            // Class 4 (IIII) 
            ( >= 5m, >= 100 and <= 10000) => 4,

            // Else Case
            _ => 0
        };
    }

   

}
