using MauiCalculator.ViewModel;

namespace MauiCalculator.Pages;

public partial class CalculatorPage : ContentPage
{
    public CalculatorPage(CalculatorViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}