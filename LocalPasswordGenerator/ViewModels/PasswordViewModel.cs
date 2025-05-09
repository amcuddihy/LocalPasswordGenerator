using LocalPasswordGenerator.Models;
using LocalPasswordGenerator.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace LocalPasswordGenerator.ViewModels;

/// <summary>
/// This is the main ViewModel for the Password Generator app.
/// It stores the properties and relay commands that the Views bind to.
/// The ViewModel also acts as a go between for the Views and Models by calling methods
/// and retrieving properties in the model classes, and then upating it's own properties, 
/// which the Views have bound themselves to. 
/// </summary>
public class PasswordViewModel : INotifyPropertyChanged
{
    private readonly PasswordGenerator _passwordGenerator;
    private readonly IUserPreferencesService _preferencesService;
    private readonly IPasswordStrengthService _passwordStrengthService;
    private PasswordSettings _passwordSettings;
    private PasswordStrengthResult _passwordStrength;

    private string _generatedPassword;
    public string GeneratedPassword 
    {
        get { 
            return _generatedPassword; 
        }
        set { 
            _generatedPassword = value; 
            OnPropertyChanged(nameof(GeneratedPassword)); 
        }
    }

    public int PasswordLength 
    {
        get { 
            return _passwordSettings.PasswordLength; 
        }
        set { 
            _passwordSettings.PasswordLength = value; 

            SavePreferences(); 
            GeneratePassword();
            OnPropertyChanged(nameof(PasswordLength)); 
        }
    }

    public string AllowedSpecialCharacters {
        get { 
            return _passwordSettings.AllowedSpecialCharacters; 
        }
        set {
            _passwordSettings.AllowedSpecialCharacters = value;
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(AllowedSpecialCharacters));
        }
    }

    public bool IncludeLowercase 
    {
        get { 
            return _passwordSettings.IncludeLowercase; 
        }
        set { 
            _passwordSettings.IncludeLowercase = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(IncludeLowercase));
        }
    }

    public bool IncludeUppercase {
        get { 
            return _passwordSettings.IncludeUppercase; 
        }
        set { 
            _passwordSettings.IncludeUppercase = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(IncludeUppercase));
        }
    }

    public bool IncludeNumbers {
        get { 
            return _passwordSettings.IncludeNumbers; 
        }
        set { 
            _passwordSettings.IncludeNumbers = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(IncludeNumbers));
        }
    }

    public bool IncludeSymbols {
        get { 
            return _passwordSettings.IncludeSymbols; 
        }
        set { 
            _passwordSettings.IncludeSymbols = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(IncludeSymbols));
        }
    }

    public PasswordStrengthResult PasswordStrength {
        get {
            return _passwordStrength;
        }
        set {
            _passwordStrength = value;
            OnPropertyChanged(nameof(PasswordStrength));
        }
    }

    public ICommand GeneratePasswordCommand { get; }
    public ICommand DefaultSpecialCharactersCommand { get; }

    /// <summary>
    /// Creates the relay commands that the views bind to. Also loads the user preferences using
    /// the preferences service that has been passed in. 
    /// </summary>
    /// <param name="preferencesService">Service to use to Save/Load user preference data</param>
    /// <param name="passwordStrengthService">Service to use to get the password strength</param>
    public PasswordViewModel(IUserPreferencesService preferencesService, IPasswordStrengthService passwordStrengthService) 
    {
        GeneratePasswordCommand = new RelayCommand(GeneratePassword);
        DefaultSpecialCharactersCommand = new RelayCommand(SetSymbolsToDefault);

        _preferencesService = preferencesService;
        _passwordSettings = _preferencesService.Load();

        _passwordGenerator = new PasswordGenerator();
        _passwordStrengthService = passwordStrengthService;
        GeneratePassword();
    }

    private void GeneratePassword() 
    {
        if (string.IsNullOrEmpty(AllowedSpecialCharacters)) {
            SetSymbolsToDefault();
        }

        if (!IncludeLowercase && !IncludeUppercase && !IncludeNumbers && !IncludeSymbols) {
            IncludeLowercase = true;
            IncludeUppercase = true;
            IncludeNumbers = true;
            IncludeSymbols = true;
        }

        GeneratedPassword = _passwordGenerator.Generate(_passwordSettings);
        PasswordStrength = _passwordStrengthService.GetPasswordStrength(GeneratedPassword);
    }

    private void SetSymbolsToDefault() 
    {
        AllowedSpecialCharacters = new PasswordSettings().AllowedSpecialCharacters;
    }

    private void SavePreferences() 
    {
        _preferencesService.Save(_passwordSettings);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) 
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
