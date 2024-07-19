using Microsoft.Extensions.Logging;
using RealStateApp.Services;

namespace RealStateApp
{
//#if DEBUG                                   // connect to local service on the
//    [Assembly: Application(UsesCleartextTraffic = true)]  // emulator's host for debugging,
//#else                                       // access via http://10.0.2.2
//[Application]                               
//#endif
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddHttpClient("api", http => http.BaseAddress = new Uri(AppSettings.ApiUrl));
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
