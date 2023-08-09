using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiCalculator.Pages;
using MauiCalculator.Classes;
using Newtonsoft.Json;

namespace MauiCalculator.ViewModel;

public partial class MainModelView : ObservableObject
{
    public MainModelView()
    {
        Items = new ObservableCollection<string>();
        
        var window = App.Window;
        window.Created += (s, e) =>
        {
            var toast = Toast.Make("Welcome :D", ToastDuration.Short, 14);
        };
        window.Resumed += (s, e) =>
        {
            var toast = Toast.Make("Welcome Back :D", ToastDuration.Short, 14);
        };
    }
    
    [ObservableProperty] 
    ObservableCollection<string> _items;

    [ObservableProperty] 
    string _text;

    private PopupService _popupService = new();
    
    [RelayCommand]
    void Add()
    {
        if(string.IsNullOrWhiteSpace(Text))
            Text = "Calculator";
        
        if (Calculator.Calculators == null)
            Calculator.Calculators = new Dictionary<string, string>();
        
        var number = 1;
        if (Items.Contains(Text))
        {
            while (Items.Contains( $"{Text} {number}"))
            {
                number++;
            }

            Text = $"{Text} {number}";
        }
            
        Calculator.Calculators.Add(Text, "0");
        Items.Add(Text);
        
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