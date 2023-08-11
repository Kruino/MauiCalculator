using System.Collections.ObjectModel;
using System.Web;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiCalculator.Pages;
using MauiCalculator.Classes;
using MauiCalculator.Handlers;

namespace MauiCalculator.ViewModel;

public partial class MainModelView : ObservableObject
{
    [ObservableProperty] 
    static ObservableCollection<string> _items;

    [ObservableProperty] 
    string _text;

    [ObservableProperty] 
    private string _value = string.Empty;
    
    private PopupService _popupService = new();
    
    public MainModelView()
    {
        Items = new ObservableCollection<string>();
        
        
        Calculator.CalculatorsChanged += OnCalculatorListChanged;
        
        var window = App.Window;
        window.Created += (s, e) =>
        {
            ToastHandler.SendToast("Welcome :D");
            DataHandler.Load();
        };
        window.Resumed += (s, e) =>
        {
            ToastHandler.SendToast("Welcome Back :D");
            DataHandler.Load();
        };
        window.Stopped += (s, e) => DataHandler.Save();
        window.Destroying += (s, e) => DataHandler.Save();
        window.Deactivated += (s, e) => DataHandler.Save();
    }
    
    private void OnCalculatorListChanged(object sender, Dictionary<string, string> e)
    {
        Items = new ObservableCollection<string>(e.Keys.ToList());
    }
    
    [RelayCommand]
    void Add(string value = null)
    {
        if(string.IsNullOrWhiteSpace(Text))
            Text = "Calculator";
        
        Calculator.AddCalculator(Text, "0");
        
        Text = string.Empty;
        
        ToastHandler.SendToast($"{Text} Created");
    }

    [RelayCommand]
    void Copy(string value = null)
    {
        if(string.IsNullOrWhiteSpace(value))
            value = "Calculator";
        
        Calculator.AddCalculator(value, Calculator.Calculators[value], true);
        ToastHandler.SendToast($"{value} Copied");
    }
    
    [RelayCommand]
    void Delete(string s)
    {
        if (Items.Contains(s))
            Items.Remove(s);

        if (Calculator.Calculators.ContainsKey(s))
            Calculator.Calculators.Remove(s);
        
        ToastHandler.SendToast($"{s} Deleted");
    }

    [RelayCommand]
    async Task Tap(string s)
    {
            Calculator.Calculators ??= new Dictionary<string, string>();

            if (Calculator.Calculators[s] == null)
                Calculator.Calculators.Add(s, "0");

            var encoded = HttpUtility.UrlEncode(Calculator.Calculators[s]);

            await Shell.Current.GoToAsync($"{nameof(CalculatorPage)}?Text={s}&NumberString={encoded}");
    }

    [RelayCommand]
    async Task GoToInfo() => await Shell.Current.GoToAsync(nameof(InfoPage));
    
}