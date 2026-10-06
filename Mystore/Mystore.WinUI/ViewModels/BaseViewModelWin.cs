using CommunityToolkit.Mvvm.ComponentModel;
using Mystore.ViewModel;
using MyStoreData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mystore.WinUI.ViewModels;

public partial class BaseViewModelWin : BaseViewModel
{
    public BaseViewModelWin(IRealmFactory realmFactory) : base(realmFactory)
    {

    }
    public Window? MainMAUIWindow { get; set; }
    [ObservableProperty]
    public partial string AppTitle { get; set; } 
}
