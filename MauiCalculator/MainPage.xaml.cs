using CommunityToolkit.Maui.Views;
using MauiCalculator.Pages;
using MauiCalculator.ViewModel;

namespace MauiCalculator;

public partial class MainPage : ContentPage
{
    
    public MainPage(MainModelView vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
    
    
}