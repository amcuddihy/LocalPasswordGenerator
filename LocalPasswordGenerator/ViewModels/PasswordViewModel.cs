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
    private UserPreferences _userPreferences;
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
            return _userPreferences.PasswordLength; 
        }
        set { 
            _userPreferences.PasswordLength = value; 

            SavePreferences(); 
            GeneratePassword();
            OnPropertyChanged(nameof(PasswordLength)); 
        }
    }

    public string AllowedSpecialCharacters {
        get { 
            return _userPreferences.AllowedSpecialCharacters; 
        }
        set {
            _userPreferences.AllowedSpecialCharacters = value;
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(AllowedSpecialCharacters));
        }
    }

    public bool AllowLowercase 
    {
        get { 
            return _userPreferences.AllowLowercase; 
        }
        set { 
            _userPreferences.AllowLowercase = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(AllowLowercase));
            OnPropertyChanged(nameof(RequireLowercase));
        }
    }

    public bool RequireLowercase {
        get { 
            return _userPreferences.RequireLowercase; 
        }
        set { 
            _userPreferences.RequireLowercase = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(RequireLowercase)); 
        }
    }

    public bool AllowUppercase {
        get { 
            return _userPreferences.AllowUppercase; 
        }
        set { 
            _userPreferences.AllowUppercase = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(AllowUppercase));
            OnPropertyChanged(nameof(RequireUppercase));
        }
    }

    public bool RequireUppercase {
        get { 
            return _userPreferences.RequireUppercase; 
        }
        set { 
            _userPreferences.RequireUppercase = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(RequireUppercase)); 
        }
    }

    public bool AllowNumbers {
        get { 
            return _userPreferences.AllowNumbers; 
        }
        set { 
            _userPreferences.AllowNumbers = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(AllowNumbers));
            OnPropertyChanged(nameof(RequireNumbers));
        }
    }

    public bool RequireNumbers {
        get { 
            return _userPreferences.RequireNumbers; 
        }
        set { 
            _userPreferences.RequireNumbers = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(RequireNumbers)); 
        }
    }

    public bool AllowSymbols {
        get { 
            return _userPreferences.AllowSymbols; 
        }
        set { 
            _userPreferences.AllowSymbols = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(AllowSymbols));
            OnPropertyChanged(nameof(RequireSymbols));
        }
    }

    public bool RequireSymbols {
        get { 
            return _userPreferences.RequireSymbols; 
        }
        set { 
            _userPreferences.RequireSymbols = value; 
            SavePreferences();
            GeneratePassword();
            OnPropertyChanged(nameof(RequireSymbols)); 
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
    /// <param name="preferencesService">The service to use to Save/Load user preference data</param>
    public PasswordViewModel(IUserPreferencesService preferencesService, IPasswordStrengthService passwordStrengthService) 
    {
        _preferencesService = preferencesService;
        _userPreferences = _preferencesService.Load();

        _passwordGenerator = new PasswordGenerator();
        GeneratedPassword = _passwordGenerator.Generate(PasswordLength, AllowedSpecialCharacters, AllowLowercase, AllowUppercase, AllowNumbers, 
                                                            AllowSymbols, RequireLowercase, RequireUppercase, RequireNumbers, RequireSymbols);

        _passwordStrengthService = passwordStrengthService;
        PasswordStrength = _passwordStrengthService.GetPasswordStrength(GeneratedPassword);

        GeneratePasswordCommand = new RelayCommand(GeneratePassword);
        DefaultSpecialCharactersCommand = new RelayCommand(SetSymbolsToDefault);
    }

    private void GeneratePassword() 
    {
        GeneratedPassword = _passwordGenerator.Generate(PasswordLength, AllowedSpecialCharacters, AllowLowercase, AllowUppercase, AllowNumbers,
                                                    AllowSymbols, RequireLowercase, RequireUppercase, RequireNumbers, RequireSymbols);
        PasswordStrength = _passwordStrengthService.GetPasswordStrength(GeneratedPassword);
    }

    private void SetSymbolsToDefault() 
    {
        AllowedSpecialCharacters = _passwordGenerator.GetDefaultSymbols();
    }

    private void SavePreferences() 
    {
        _preferencesService.Save(_userPreferences);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName) 
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
