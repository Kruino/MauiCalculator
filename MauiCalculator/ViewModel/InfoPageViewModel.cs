using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiCalculator.Classes;

namespace MauiCalculator.ViewModel;

public partial class InfoPageViewModel : ObservableObject
{
    [ObservableProperty] private ObservableCollection<string> _items = new();
    [ObservableProperty] private string _text;

    public InfoPageViewModel()
    {
        Items.Add(Calculator.GetName("Calculator", Items.ToList()));
    }
}