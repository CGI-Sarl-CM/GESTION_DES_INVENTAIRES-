global using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGIERPData.ModelViews.LaboratoryDepartment;

public partial class BalanceClass : ObservableObject
{


    public int ClassNumber { get; set; }
    public bool EstEnVerificationPeriodique { get; set; }
    public bool EstEnVerificationPrimitive { get; set; }
    public decimal EchelonNumbers { get; set; }
    public decimal EchelonVerif { get; set; }
    public decimal EMT { get; set; }
    public decimal MaximumWeight { get; set; }
    public string? Status { get; set; }


}
