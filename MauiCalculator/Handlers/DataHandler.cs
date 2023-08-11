using MauiCalculator.Classes;
using Newtonsoft.Json;

namespace MauiCalculator.Handlers;

public abstract class DataHandler
{
    private static readonly string FullPath = Path.Combine(FileSystem.Current.AppDataDirectory, "data.json");
    
    public static void Save()
    {
        var json = JsonConvert.SerializeObject(Calculator.Calculators);
        
        File.WriteAllText(FullPath,  json);
    }

    public static void Load()
    {
        if (!File.Exists(FullPath)) return;
        
        var json = File.ReadAllText(FullPath);
            
        Calculator.Calculators = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
    }
}