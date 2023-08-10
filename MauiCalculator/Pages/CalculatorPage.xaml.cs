using MauiCalculator.ViewModel;

namespace MauiCalculator.Pages;

public partial class CalculatorPage : ContentPage
{
    public CalculatorPage(CalculatorViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        
        SizeChanged += OnSizeChanged;
        
        var window = App.Window;
    }
    
    private void OnSizeChanged(object sender, EventArgs e)
    {
        if (App.Window.Width < App.Window.Height) 
        {
            Grid.SetRow(BackButton, 0);
            Grid.SetColumn(BackButton, 1);
            Grid.SetColumnSpan(BackButton, 1);
            Grid.SetRowSpan(BackButton, 2);
            
            Grid.SetColumnSpan(AcButton, 1);
            Grid.SetRowSpan(AcButton, 2);
        }
        else 
        {
            Grid.SetRow(BackButton, 1);
            Grid.SetColumn(BackButton, 0);
            Grid.SetColumnSpan(BackButton, 2);
            Grid.SetRowSpan(BackButton, 1);
            
            Grid.SetColumnSpan(AcButton, 2);
            Grid.SetRowSpan(AcButton, 1);
        }
    }
}