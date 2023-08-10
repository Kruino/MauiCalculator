using System.Data;
using System.Text.RegularExpressions;

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

        public static string Calculate(string expression)
        {
                expression = Regex.Replace(expression, @"(\d+)\(([^)]+)\)", "$1*$2");
                
                var table = new DataTable();
                table.Columns.Add("expression", typeof(string), expression);
        
                var row = table.NewRow();
                table.Rows.Add(row);
        
                var resultValue = Convert.ToDouble(row["expression"]);
                var result = (resultValue % 1 == 0) ? resultValue.ToString("0") : resultValue.ToString("0.00");
                return result;
        }
}

