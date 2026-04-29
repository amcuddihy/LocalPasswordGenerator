using LocalPasswordGenerator.Models;
using Newtonsoft.Json;
using System.IO;

namespace LocalPasswordGenerator.Services;

public class JsonUserPreferencesService : IUserPreferencesService
{
    private static readonly string FileName = "user_prefs.json";
    private string FilePath { get; }

    public JsonUserPreferencesService() 
    {
        FilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LocalPasswordGenerator");
        if(!Directory.Exists(FilePath)) 
        {
            Directory.CreateDirectory(FilePath);
        }
        // Doing this after ensuring the directory exists helps to simplify the code.
        // Otherwise the file name would have to be removed to check for the directory, then added back to get the full file path.
        FilePath = Path.Combine(FilePath, FileName);
    }

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
            return new PasswordSettings(); // Since there are very few options for preferences, returning default settings is preferable to crashing the app. 
                                           // A user can manually return to their preferred settings in literally seconds. 
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
