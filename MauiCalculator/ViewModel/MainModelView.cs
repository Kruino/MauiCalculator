using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiCalculator.Pages;
using MauiCalculator.Classes;

namespace MauiCalculator.ViewModel;

public partial class MainModelView : ObservableObject
{
    public MainModelView()
    {
        Items = new ObservableCollection<string>();
        Calculator.CalculatorsChanged += OnCalculatorListChanged;
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        
        var window = App.Window;
        window.Created += async (s, e) =>
        {
            var toast = Toast.Make("Welcome :D", ToastDuration.Short, 14);
            await toast.Show(cancellationTokenSource.Token);
        };
        window.Resumed += async (s, e) =>
        {
            var toast = Toast.Make("Welcome Back :D", ToastDuration.Short, 14);
            await toast.Show(cancellationTokenSource.Token);
        };
    }

    private void OnCalculatorListChanged(object sender, Dictionary<string, string> e)
    {
        Items = new ObservableCollection<string>(e.Keys.ToList());
    }
    
    [ObservableProperty] 
    static ObservableCollection<string> _items;

    [ObservableProperty] 
    string _text;

    [ObservableProperty] 
    private string _value = string.Empty;
    
    private PopupService _popupService = new();
    
    [RelayCommand]
    void Add(string value = null)
    {
        if(string.IsNullOrWhiteSpace(Text))
            Text = "Calculator";
        
        Calculator.AddCalculator(Text, "0");
        
        Text = string.Empty;
    }

    [RelayCommand]
    void Delete(string s)
    {
        if (Items.Contains(s))
            Items.Remove(s);

        if (Calculator.Calculators.ContainsKey(s))
            Calculator.Calculators.Remove(s);
    }

    [RelayCommand]
    async Task Tap(string s)
    {
            Calculator.Calculators ??= new Dictionary<string, string>();

            if (Calculator.Calculators[s] == null)
                Calculator.Calculators.Add(s, "0");
            
            await Shell.Current.GoToAsync($"{nameof(CalculatorPage)}?Text={s}&NumberString={Calculator.Calculators[s]}");
    }
}