using MyStoreData;
using MyStoreData.Enums;
using MyStoreData.Models;
using MyStoreData.Models.EmbeddedObjects;
using MyStoreData.Models.QHSEModels;
using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mystore.ViewModel;

public partial class BaseViewModel : ObservableObject, IDisposable
{
    protected readonly IRealmFactory _realmFactory;
    public IRealmFactory RealmFactory => _realmFactory;
    public BaseViewModel(IRealmFactory realmFactory)
    {
                _realmFactory = realmFactory;
    }

    [ObservableProperty]
    public partial string Title { get; set; }

    public static string CurrentAppVersion { get; }
    public static string CurrentAppStage { get; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
