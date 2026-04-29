using LocalPasswordGenerator.Models;
using LocalPasswordGenerator.Services;

namespace LocalPasswordGenerator.Tests;

public class PasswordTests 
{
    [Fact]
    public void CanGeneratePasswordWithDefaultSettings() {
        var passwordSettings = new PasswordSettings();
        passwordSettings.PasswordLength = 12;
        
        var passwordGeneratorService = new SimplePasswordGeneratorService();
        var password = passwordGeneratorService.GeneratePassword(passwordSettings);

        Assert.Equal(12, password.Length);
    }

    [Fact]
    public void GeneratesPassword_WithAllCharacterTypes() { 
        var passwordSettings = new PasswordSettings();
        passwordSettings.IncludeLowercase = true;
        passwordSettings.IncludeUppercase = true;
        passwordSettings.IncludeNumbers = true;
        passwordSettings.IncludeSymbols = true;

        var passwordGeneratorService = new SimplePasswordGeneratorService();
        var password = passwordGeneratorService.GeneratePassword(passwordSettings);

        Assert.Contains(password, c => char.IsLower(c));
        Assert.Contains(password, c => char.IsUpper(c));
        Assert.Contains(password, c => char.IsDigit(c));
        Assert.Contains(password, c => passwordSettings.AllowedSpecialCharacters.Contains(c));
    }
}