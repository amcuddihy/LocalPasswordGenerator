

using Newtonsoft.Json;
using System.ComponentModel;

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
public class UserPreferences
{
    public int PasswordLength { 
        get; 
        set; 
    } = 12;

    public string AllowedSpecialCharacters { 
        get; 
        set; 
    } = "!@#$%^&*()_-+=<>?";

    public bool AllowLowercase { 
        get; 
        set; 
    } = true;

    private bool _requireLowercase = true;
    public bool RequireLowercase {
        get { 
            return _requireLowercase && AllowLowercase; 
        }
        set { 
            _requireLowercase = value; 
        }
    }
    
    public bool AllowUppercase { 
        get; 
        set; 
    } = true;

    private bool _requireUppercase = true;
    public bool RequireUppercase {
        get { 
            return _requireUppercase && AllowUppercase; 
        }
        set { 
            _requireUppercase = value; 
        }
    }

    public bool AllowNumbers { 
        get; 
        set; 
    } = true;

    private bool _requireNumbers = true;
    public bool RequireNumbers {
        get { 
            return _requireNumbers && AllowNumbers; 
        }
        set { 
            _requireNumbers = value; 
        }
    }

    public bool AllowSymbols { 
        get; 
        set; 
    } = true;

    private bool _requireSymbols = true;
    public bool RequireSymbols {
        get { 
            return _requireSymbols && AllowSymbols; 
        }
        set { 
            _requireSymbols = value; 
        }
    }

}
