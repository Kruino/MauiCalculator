using System.Data;
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
    
    private PopupService _popupService = new();
    
    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
        
    }
    
    [RelayCommand]
    private void AddToNumber(string s)
    {
        if (NumberString.Contains("Error") || NumberString == "0")
            NumberString = string.Empty;

        if (s.Contains("AC"))
            NumberString = "0";
        else if (s.Contains('='))
        {
            try
            {
                var value = new DataTable().Compute(NumberString, null).ToString();
                if (value != null) NumberString = value;
               
                if(value is "69" or "80085")
                    _popupService.ShowPopup(new PopupPage("Nice!!"));
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
}