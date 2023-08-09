namespace MauiCalculator.Classes;

public static class Calculator
{
    public static event EventHandler<Dictionary<string, string>> CalculatorsChanged;
    private static Dictionary<string, string> _calculators = new();

    public static Dictionary<string, string> Calculators
    {
        get => _calculators;
        set
        {
            _calculators = value;
            CalculatorsChanged?.Invoke(null, _calculators);
        }
    }

    public static string AddCalculator(string key, string value)
    {
        var number = 1;
        if (Calculators.Keys.Contains(key))
        {
            while (Calculators.Keys.Contains( $"{key} {number}"))
            {
                number++;
            }

            key = $"{key} {number}";
        }
        
        Dictionary<string, string> updatedDictionary = new Dictionary<string, string>(Calculators)
        {
            { key, value }
        };

        Calculators = updatedDictionary; // This will trigger the event

        return key;
    }
}

