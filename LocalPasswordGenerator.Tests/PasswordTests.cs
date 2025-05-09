using LocalPasswordGenerator.Models;

namespace LocalPasswordGenerator.Tests;

public class PasswordTests 
{
    [Fact]
    public void GeneratesPassword_WithCorrectLength() {
        var passwordSettings = new PasswordSettings();
        passwordSettings.PasswordLength = 12;
        
        var password = new PasswordGenerator().Generate(passwordSettings);

        Assert.Equal(12, password.Length);
    }

    [Fact]
    public void GeneratesPassword_WithAllCharacterTypes() { 
        var passwordSettings = new PasswordSettings();
        passwordSettings.IncludeLowercase = true;
        passwordSettings.IncludeUppercase = true;
        passwordSettings.IncludeNumbers = true;
        passwordSettings.IncludeSymbols = true;

        var password = new PasswordGenerator().Generate(passwordSettings);

        Assert.Contains(password, c => char.IsLower(c));
        Assert.Contains(password, c => char.IsUpper(c));
        Assert.Contains(password, c => char.IsDigit(c));
        Assert.Contains(password, c => passwordSettings.AllowedSpecialCharacters.Contains(c));
    }
}