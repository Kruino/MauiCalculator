using MauiCalculator.Pages;

namespace MauiCalculator;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(CalculatorPage), typeof(CalculatorPage));
        Routing.RegisterRoute(nameof(InfoPage), typeof(InfoPage));
    }
}