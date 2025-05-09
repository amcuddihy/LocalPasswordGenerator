using LocalPasswordGenerator.Models;

namespace LocalPasswordGenerator.Services;

/// <summary>
/// Interface that any class that is meant to be used as a User Preferences Service must implement.
/// This allows any class that implements the interface to be easily dependency injected into the 
/// PasswordViewModel as the service to be used to load the user preferences.
/// </summary>
public interface IUserPreferencesService
{
    PasswordSettings Load();
    void Save(PasswordSettings preferences);
}
