using CommunityToolkit.Maui.Views;
using MauiCalculator.Interfaces;

namespace MauiCalculator.Classes;

public class PopupService : IPopupService
{
    public void ShowPopup(Popup popup)
    {
        Page page = Application.Current?.MainPage ?? throw new NullReferenceException();
        page.ShowPopup(popup);
    }
}