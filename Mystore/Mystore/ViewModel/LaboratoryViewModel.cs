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
using System.Globalization;
using System.Text;

namespace CGIERP.ViewModel;

public partial class LaboratoryViewModel : BaseViewModel
{
    [ObservableProperty]
    public partial BalanceClass CurrentBalance { get; set; }

    [ObservableProperty]
    public partial ObservableCollection<string> MassUnitsCollection { get; set; }

    [ObservableProperty]
    public partial string SelectedPMaxUnit { get; set; }

    [ObservableProperty]
    public partial string SelectedEchelonUnit { get; set; }

    [ObservableProperty]
    public partial string SelectedAppliedMassUnit { get; set; }

    //[ObservableProperty]
    //public partial string SelectedEchelonUnit { get; set; }

    [ObservableProperty]
    public partial string MaximumWeight { get; set; }

    [ObservableProperty]
    public partial string EchelonVerif { get; set; }

    [ObservableProperty]
    public partial decimal MassValue { get; set; }

    [ObservableProperty]
    public partial string ErrorValue { get; set; }

    [ObservableProperty]
    public partial string ConformityStatus { get; set; } = string.Empty;

    [ObservableProperty]
    public partial ObservableCollection<string> VerificationsTypeCollection { get; set; }

    [ObservableProperty]
    public partial string SelectedVerificationsType { get; set; }

    public LaboratoryViewModel(IRealmFactory realmFactory) : base(realmFactory)
    {
        CurrentBalance = new();
        VerificationsTypeCollection = new();
        MassUnitsCollection = new();
        VerificationsTypeCollection.AddRange(new List<string> { "Verification Primitive", "Verification Periodique" });
        MassUnitsCollection.AddRange(new List<string> { "mg", "g", "kg", "T" });

        SelectedEchelonUnit = MassUnitsCollection[1];
        SelectedPMaxUnit = MassUnitsCollection[1];
        EchelonVerif = string.Empty;
        MaximumWeight = string.Empty;
        ErrorValue = string.Empty;
        SelectedVerificationsType = VerificationsTypeCollection.First();
    }

    [RelayCommand]
    private async Task DetermineNumberOfEchelons()
    {
        try
        {
            
            if (!decimal.TryParse(MaximumWeight?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal maxWeightInG))
                throw new Exception("Poids maximum invalide.");

            if (!decimal.TryParse(EchelonVerif?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal echVerifInG))
                throw new Exception("Échelon de vérification invalide.");

            if (!decimal.TryParse(ErrorValue?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal errorValue))
                throw new Exception("Valeur d'écart invalide.");

            maxWeightInG = SelectedPMaxUnit.ToLower() switch
            {
                "mg" => maxWeightInG / 1000m,
                "kg" => maxWeightInG * 1000m,
                "t" => maxWeightInG * 1000000m,
                _ => maxWeightInG
            };

            echVerifInG = SelectedEchelonUnit.ToLower() switch
            {
                "mg" => echVerifInG / 1000m,
                "kg" => echVerifInG * 1000m,
                "t" => echVerifInG * 1000000m,
                _ => echVerifInG
            };

            decimal numberOfEchelons = maxWeightInG / echVerifInG;
            CurrentBalance.EchelonNumbers = numberOfEchelons;
            // 1. Classify
            AutoClassifyBalance(numberOfEchelons, maxWeightInG, echVerifInG);

            // 2. Minimum Weight
            CalculateMinWeight(echVerifInG, numberOfEchelons);

            // 3. EMT
            CalculerEMT(MassValue, echVerifInG);

            // 4. Status
            CalculateStatus(errorValue);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlertAsync("Erreur", ex.Message, "OK");
        }
    }

    private void CalculateStatus(decimal errorValue)
    {
        // Metrology rule: Errors can be negative. We MUST use Math.Abs (Absolute value) 
        // to compare the magnitude of the error against the EMT.
        if (Math.Abs(errorValue) > CurrentBalance.EMT)
        {
            ConformityStatus = "Non conforme";

            CurrentBalance.Status = "Non conforme"; 
        }
        else
        {
            ConformityStatus = "Conforme";

            CurrentBalance.Status = "Conforme";
        }
    }

    private void CalculerEMT(decimal massValue, decimal echVerifInG)
    {
        if (echVerifInG <= 0m) return;

        decimal mInE = massValue / echVerifInG;

        decimal factor = CurrentBalance.ClassNumber switch
        {
            1 => mInE switch { <= 50000m => 0.5m, <= 200000m => 1.0m, _ => 1.5m },
            2 => mInE switch { <= 5000m => 0.5m, <= 20000m => 1.0m, _ => 1.5m },
            3 => mInE switch { <= 500m => 0.5m, <= 2000m => 1.0m, _ => 1.5m },
            4 => mInE switch { <= 50m => 0.5m, <= 200m => 1.0m, _ => 1.5m },
            _ => 0m
        };

        decimal verificationMultiplier = SelectedVerificationsType == "Verification Periodique" ? 2.0m : 1.0m;
        CurrentBalance.EMT = factor * echVerifInG * verificationMultiplier;
    }

    private void CalculateMinWeight(decimal echVerifInG, decimal numbOfEchelons)
    {
        // Rewritten cleanly using standard pattern matching!
        CurrentBalance.MinimumWeight = CurrentBalance.ClassNumber switch
        {
            1 => 100m * echVerifInG,

            // Class 2 rules
            2 when echVerifInG >= 0.001m && echVerifInG <= 0.05m && numbOfEchelons is >= 100m and <= 100000m
                => 20m * echVerifInG,
            2 => 50m * echVerifInG,

            3 => 20m * echVerifInG,
            4 => 10m * echVerifInG,
            _ => 0m
        };
    }

    private void AutoClassifyBalance(decimal numberOfEch, decimal maxWeightInG, decimal echVerifInG)
    {
        CurrentBalance.ClassNumber = (echVerifInG, numberOfEch) switch
        {
            ( >= 0.0001m, >= 50000m) => 1,

            ( >= 0.001m and <= 0.05m, >= 100m and <= 100000m) => 2,
            ( >= 0.1m, >= 5000m and <= 100000m) => 2,

            ( >= 0.1m and <= 2m, >= 100m and <= 10000m) => 3,
            ( >= 5m, >= 500m and <= 10000m) => 3,

            //Class 4 upper bound changed back to 1000 based on OIML R76!
            ( >= 5m, >= 100m and <= 1000m) => 4,

            _ => 0
        };
    }
}