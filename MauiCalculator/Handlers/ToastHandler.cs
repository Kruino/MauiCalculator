using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace MauiCalculator.Handlers;

public static class ToastHandler
{
    private static readonly CancellationTokenSource CancellationTokenSource = new();
    
    public static async void SendToast(string text)
    {
        var toast = Toast.Make(text, ToastDuration.Short, 14);
        await toast.Show(CancellationTokenSource.Token);
    }
}