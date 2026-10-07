using CommunityToolkit.Mvvm.ComponentModel;
using CGIERP.ViewModel;
using CGIERPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGIERP.WinUI.ViewModels;

public partial class BaseViewModelWin : BaseViewModel
{
    public BaseViewModelWin(IRealmFactory realmFactory) : base(realmFactory)
    {

    }
    public Window? MainMAUIWindow { get; set; }
    [ObservableProperty]
    public partial string AppTitle { get; set; } 
}
