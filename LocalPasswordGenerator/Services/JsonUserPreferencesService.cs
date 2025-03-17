using LocalPasswordGenerator.Models;
using Newtonsoft.Json;
using System.IO;

namespace LocalPasswordGenerator.Services;

public class JsonUserPreferencesService : IUserPreferencesService
{
    private static readonly string FilePath = "user_prefs.json";

    public UserPreferences Load() 
    {
        try 
        {
            if (File.Exists(FilePath)) 
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<UserPreferences>(json) ?? new UserPreferences();
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error loading preferences: {ex.Message}");
        }
        return new UserPreferences();
    }

    public void Save(UserPreferences preferences) 
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
