using System.Data;
using System.Text.RegularExpressions;
using CommunityToolkit.Mvvm.ComponentModel;

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

        public static string AddCalculator(string key, string value, bool copy = false)
        {

            var name = GetName(key, Calculators.Keys.ToList(), copy); 
            Dictionary<string, string> updatedDictionary = new Dictionary<string, string>();
                
            if(!copy)
                updatedDictionary.Add(name, value);

            foreach (var item in Calculators)
            {
                updatedDictionary.Add(item.Key, item.Value);
                if (item.Key == key && copy)
                    updatedDictionary.Add(name, value);
            }
                
            Calculators = updatedDictionary;

            return name;
        }

        public static string GetName(string name, List<string> names, bool copy = false)
        {
            var key = name;
            if (copy)
            {
                name = $"{name} Copy";
            }
                
            var number = 1;
            if (names.Contains(name))
            {
                while (names.Contains( $"{name} {number}"))
                {
                    number++;
                }

                name = $"{name} {number}";
            }

            return name;
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

