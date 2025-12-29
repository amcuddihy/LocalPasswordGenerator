using LocalPasswordGenerator.Models;
using Newtonsoft.Json;
using System.IO;

namespace LocalPasswordGenerator.Services;

public class JsonUserPreferencesService : IUserPreferencesService
{
    private static readonly string FilePath = "user_prefs.json";

    public PasswordSettings Load() 
    {
        try {
            var rawJsonText = File.ReadAllText(FilePath);
            var userSettings = JsonConvert.DeserializeObject<PasswordSettings>(rawJsonText);

            return userSettings is null
                ? new PasswordSettings()
                : userSettings;
        }
        catch (Exception ex) {
            Console.WriteLine($"Error loading preferences: {ex.Message}");
            return new PasswordSettings();
        }
    }

    public void Save(PasswordSettings preferences) 
    {
        try 
        {
            string json = JsonConvert.SerializeObject(preferences, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error saving preferences: {ex.Message}");
        }
    }
}
