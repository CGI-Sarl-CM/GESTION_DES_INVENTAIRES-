global using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGIERPData.ModelViews.LaboratoryDepartment;

public partial class BalanceClass : ObservableObject
{

    [ObservableProperty]
    public partial int ClassNumber { get; set; }

    [ObservableProperty]
    public partial decimal MinimumWeight { get; set; }
    public bool EstEnVerificationPeriodique { get; set; }

    public decimal EchelonNumbers { get; set; }
    public decimal EchelonVerif { get; set; }
    public decimal EMT { get; set; }
    public decimal MaximumWeight { get; set; }
    public string? Status { get; set; }


}
