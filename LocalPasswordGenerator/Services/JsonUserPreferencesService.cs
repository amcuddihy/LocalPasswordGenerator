using LocalPasswordGenerator.Models;
using Newtonsoft.Json;
using System.IO;

namespace LocalPasswordGenerator.Services;

/// <summary>
/// Uses the Newtonsoft.Json library to store the user settings as a JSON file.
/// Implements the IUserPreferencesService interface so that it can be dependency
/// injected into the PasswordViewModel.
/// </summary>
public class JsonUserPreferencesService : IUserPreferencesService
{
    private static readonly string FilePath = "user_prefs.json";

    /// <summary>
    /// Loads the user preferences from a JSON file. 
    /// </summary>
    /// <returns>The user preferences as a UserPreferences object</returns>
    public PasswordSettings Load() 
    {
        try 
        {
            if (File.Exists(FilePath)) 
            {
                string json = File.ReadAllText(FilePath);
                return JsonConvert.DeserializeObject<PasswordSettings>(json) ?? new PasswordSettings();
            }
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Error loading preferences: {ex.Message}");
        }
        return new PasswordSettings();
    }

    /// <summary>
    /// Saves the user preferences as a JSON file.
    /// </summary>
    /// <param name="preferences">User preferences object to be serialized into JSON and saved.</param>
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
