using System.Web;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiCalculator.Classes;
using MauiCalculator.Handlers;
using MauiCalculator.Pages;

namespace MauiCalculator.ViewModel;

[QueryProperty("Text", "Text")]
[QueryProperty("NumberString", "NumberString")]
public partial class CalculatorViewModel : ObservableObject
{
    [ObservableProperty]
    private string _text;
    
    [ObservableProperty]
    private string _numberString;
    
    [ObservableProperty]
    private string _lastNumber;

    private bool _lastInputCalculate = false;
    
    private readonly PopupService _popupService = new();
    
    public CalculatorViewModel()
    {
        NumberString = HttpUtility.HtmlDecode(NumberString);
    }
    
    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private void DeleteNumber()
    {
        NumberString = "0";
    }

    [RelayCommand]
    private void RemoveLast()
    {
        if (NumberString != string.Empty && NumberString != "0")
            NumberString = NumberString.Substring(0, NumberString.Length - 1);
        else
            NumberString = "0";
    }
    
    [RelayCommand]
    private void AddToNumber(string s)
    {
        if (s != "+" && s != "-" && s != "*" && s != "/" && s != "%")
        {
            if (_lastInputCalculate)
            {
                LastNumber = NumberString;
                NumberString = string.Empty;
            }
            else if (NumberString == "0")
            {
                NumberString = string.Empty;
            }
        }
        
        _lastInputCalculate = false;
        
        if (NumberString.Contains("Error"))
            NumberString = string.Empty;
    
        if (s.Contains('='))
        {
            try
            {
                var value = Calculator.Calculate(NumberString);
                NumberString = value;

                if(value is "69" or "80085")
                    _popupService.ShowPopup(new PopupPage("Nice!!"));
                
                
                _lastInputCalculate = true;
            }
            catch
            {
                NumberString = "Error";
            }
            
        }
        else
            NumberString += s;
        
        Calculator.Calculators[Text] = NumberString;
    }

    [RelayCommand]
    private void CreateNewWithValue()
    {
        ToastHandler.SendToast($"{Text} Copied");
        Text = Calculator.AddCalculator(Text, NumberString, true);
        
    }
}