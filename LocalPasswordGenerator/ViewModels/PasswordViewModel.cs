using LocalPasswordGenerator.Models;
using LocalPasswordGenerator.Services;
using System.ComponentModel;
using System.Windows.Input;

namespace LocalPasswordGenerator.ViewModels;

public class PasswordViewModel : INotifyPropertyChanged
{
    private readonly PasswordGenerator _passwordGenerator;
    private readonly IUserPreferencesService _preferencesService;
    private UserPreferences _userPreferences;

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
            OnPropertyChanged(nameof(RequireSymbols)); 
        }
    }

    public ICommand GeneratePasswordCommand { get; }
    public ICommand DefaultSpecialCharactersCommand { get; }

    public PasswordViewModel(IUserPreferencesService preferencesService) 
    {
        _passwordGenerator = new PasswordGenerator();
        _preferencesService = preferencesService;
        _userPreferences = _preferencesService.Load();

        GeneratePasswordCommand = new RelayCommand(GeneratePassword);
        DefaultSpecialCharactersCommand = new RelayCommand(SetSymbolsToDefault);
    }

    private void GeneratePassword() 
    {
        GeneratedPassword = _passwordGenerator.Generate(PasswordLength, AllowedSpecialCharacters, AllowLowercase, AllowUppercase, AllowNumbers, AllowSymbols,
                                                                                                    RequireLowercase, RequireUppercase, RequireNumbers, RequireSymbols);
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
