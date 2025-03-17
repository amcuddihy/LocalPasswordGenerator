

using System.ComponentModel;

namespace LocalPasswordGenerator.Models;

// The UI should prevent someone from requiring a char set while not allowing it. The "Require Properties" returning
// the require and allow AND'd together is a safety check in case a user edits the JSON file manually.
public class UserPreferences
{
    public int PasswordLength { get; set; } = 12;

    public string AllowedSpecialCharacters { get; set; } = "!@#$%^&*()_-+=<>?";

    public bool AllowLowercase { get; set; } = true;

    private bool _requireLowercase = true;
    public bool RequireLowercase {
        get { return _requireLowercase && AllowLowercase; }
        set { _requireLowercase = value; }
    }
    
    public bool AllowUppercase { get; set; } = true;

    private bool _requireUppercase = true;
    public bool RequireUppercase {
        get { return _requireUppercase && AllowUppercase; }
        set { _requireUppercase = value; }
    }

    public bool AllowNumbers { get; set; } = true;

    private bool _requireNumbers = true;
    public bool RequireNumbers {
        get { return _requireNumbers && AllowNumbers; }
        set { _requireNumbers = value; }
    }

    public bool AllowSymbols { get; set; } = true;

    private bool _requireSymbols = true;
    public bool RequireSymbols {
        get { return _requireSymbols && AllowSymbols; }
        set { _requireSymbols = value; }
    }

}
