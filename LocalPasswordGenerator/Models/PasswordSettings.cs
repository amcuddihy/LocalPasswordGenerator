
namespace LocalPasswordGenerator.Models;

/// <summary>
/// Data model that stores the user preferences that are set in the UI.
/// Does not directly handle the saving and loading of these preferences,
/// that is handled by a dependency injected IUserPreferences service.
/// 
/// To prevent a possible crash, the Require{CharType} variables are and'd with their respective 
/// Allow{CharType} variables before being returned.This ensures require can't be true if allow isn't
/// true. The UI prevents this from happening under normal operation of the software, this check
/// is just in case someone edits the user preferences file manually. 
/// </summary>
public class PasswordSettings
{
    public int PasswordLength { get; set; } = 12;

    public string AllowedSpecialCharacters { get; set; } = "!@#$%^&*()_-+=<>?";

    public bool IncludeLowercase { get; set; } = true;
    
    public bool IncludeUppercase { get; set; } = true;

    public bool IncludeNumbers { get; set; } = true;

    public bool IncludeSymbols { get; set; } = true;

    public int CrackSpeedSetting { get; set; } = 1; // 0-3, 0 = Fast, 3 = Slow
}
