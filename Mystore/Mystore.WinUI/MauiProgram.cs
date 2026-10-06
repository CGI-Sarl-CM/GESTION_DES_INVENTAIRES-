using Mystore.WinUI.Services;

namespace Mystore.WinUI
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
                builder.Services.AddSingleton<BaseViewModelWin>();
            return builder.Build();
        }
    }
}
