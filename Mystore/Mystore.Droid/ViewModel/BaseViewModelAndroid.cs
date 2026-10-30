using CGIERPData;
using System;
using System.Collections.Generic;
using System.Text;

namespace CGIERP.ViewModel;

public partial class BaseViewModelAndroid : BaseViewModel
{
    public BaseViewModelAndroid(IRealmFactory realmFactory) : base(realmFactory)
    {
    }
}
