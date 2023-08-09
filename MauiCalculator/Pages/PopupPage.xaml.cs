using CommunityToolkit.Maui.Views;
using MauiCalculator.ViewModel;

namespace MauiCalculator.Pages;

public partial class PopupPage : Popup
{
    public PopupPage(string text)
    {
        InitializeComponent();
        MainLabel.Text = text;
    }
}