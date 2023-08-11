using CommunityToolkit.Maui;
using MauiCalculator.Pages;
using MauiCalculator.ViewModel;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;

namespace MauiCalculator;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>().UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainModelView>();
        
        builder.Services.AddTransient<CalculatorPage>();
        builder.Services.AddTransient<CalculatorViewModel>();
        
        builder.Services.AddTransient<InfoPage>();
        builder.Services.AddTransient<InfoPageViewModel>();
        return builder.Build();
    }
}