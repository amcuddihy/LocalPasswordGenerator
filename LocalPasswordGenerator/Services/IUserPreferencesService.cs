using LocalPasswordGenerator.Models;

namespace LocalPasswordGenerator.Services;

// Making the user preferences service an interface instead of a class allows a different service
// to be easily dependency injected, the service class just needs to implement this interface.
public interface IUserPreferencesService
{
    UserPreferences Load();
    void Save(UserPreferences preferences);
}
