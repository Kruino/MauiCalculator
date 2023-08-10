using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiCalculator.Classes;
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
    
    private PopupService _popupService = new();
    
    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
        
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


        if (s.Contains("AC"))
            NumberString = "0";
        else if(s.Contains("Back"))
        {
            if (NumberString != string.Empty)
                NumberString = NumberString.Substring(0, NumberString.Length - 1);
        }
        else if (s.Contains('='))
        {
            try
            {
                string value = Calculator.Calculate(NumberString);
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
    private async Task CreateNewWithValue()
    {
        var name = Calculator.AddCalculator($"{Text} Copy", NumberString);
        
        var currentPage = Shell.Current;
        await currentPage.GoToAsync($"{nameof(CalculatorPage)}?Text={name}&NumberString={NumberString}");
        
        var previousPage = currentPage.Navigation.NavigationStack[^2]; // Get the second-to-last page
        currentPage.Navigation.RemovePage(previousPage);
    }
}