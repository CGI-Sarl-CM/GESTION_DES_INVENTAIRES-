using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using CGIERP.ViewModel;
using CGIERPData;
using Material.Components.Maui.Extensions;
using Syncfusion.Maui.Toolkit.Hosting;

namespace CGIERP;

public static class MauiProgramExtensions
{
    public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
    {
        builder
            .UseMauiApp<App>()
            .ConfigureSyncfusionToolkit()
            .UseMaterialComponents()
            .UseMauiCommunityToolkit( op=>
            {

            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder.Services.AddSingleton<IRealmFactory, RealmFactory>();
        builder.Services.AddSingleton<ConnexionViewModel>();
        builder.Services.AddSingleton<BaseViewModel>();
        builder.Services.AddSingleton<QhseViewModel>();
        builder.Services.AddSingleton<LaboratoryViewModel>();
        builder.Services.AddSingleton<MainPage>();

        return builder;
    }
}
