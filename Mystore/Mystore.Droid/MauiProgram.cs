using CGIERP.Services;

namespace CGIERP.Droid
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseSharedMauiApp();
            builder
                .Services.AddSingleton<IAppUtil, AppUtil>();
            builder.Services.AddSingleton<BaseViewModelAndroid>();
            return builder.Build();
        }
    }
}
