using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Mystore.ViewModel;
using MyStoreData;

namespace Mystore
{
    public static class MauiProgramExtensions
    {
        public static MauiAppBuilder UseSharedMauiApp(this MauiAppBuilder builder)
        {
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
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
            builder.Services.AddSingleton<MainPage>();

            return builder;
        }
    }
}
