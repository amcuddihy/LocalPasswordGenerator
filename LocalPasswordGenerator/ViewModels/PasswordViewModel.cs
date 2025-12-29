using LocalPasswordGenerator.Models;
using LocalPasswordGenerator.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace LocalPasswordGenerator.ViewModels;

public class PasswordViewModel : INotifyPropertyChanged
{
    private readonly IPasswordGeneratorService _passwordGenerator;
    private readonly IUserPreferencesService _preferencesService;

    private PasswordSettings _passwordSettings;

    private int _previousPasswordLength = -1;

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
            if (value == _previousPasswordLength) {
                return; // The slider is firing multiple times per increment/decrement
            }
            _previousPasswordLength = value;

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

    public ICommand GeneratePasswordCommand { get; }
    public ICommand DefaultSpecialCharactersCommand { get; }

    public PasswordViewModel(IUserPreferencesService preferencesService, IPasswordGeneratorService passwordGenerator) 
    {
        GeneratePasswordCommand = new RelayCommand(GeneratePassword);
        DefaultSpecialCharactersCommand = new RelayCommand(SetSymbolsToDefault);

        _preferencesService = preferencesService;
        _passwordSettings = _preferencesService.Load(); // this is the only time this is done, so no wrapper function is used

        _passwordGenerator = passwordGenerator;
        GeneratePassword();
    }

    private void GeneratePassword() 
    {
        // Reset to default special characters if the field is empty
        // If the user doesn't want special characters, they should uncheck the include check box
        // Otherwise the password generator will fail and the user will be confused by the empty text box 
        if (string.IsNullOrEmpty(AllowedSpecialCharacters)) {
            SetSymbolsToDefault();
        }

        // Ensure at least one character type is selected. This is assuming the user did this in error or doesn't understand what it means.
        // Resetting to all character types as that is the most likely configuration and it is tedious to reselect each box individually.
        if (!IncludeLowercase && !IncludeUppercase && !IncludeNumbers && !IncludeSymbols) {
            IncludeLowercase = true;
            IncludeUppercase = true;
            IncludeNumbers = true;
            IncludeSymbols = true;
        }

        GeneratedPassword = _passwordGenerator.GeneratePassword(_passwordSettings);
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
