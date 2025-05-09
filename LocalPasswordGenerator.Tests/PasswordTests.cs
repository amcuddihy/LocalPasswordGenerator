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
        passwordSettings.AllowLowercase = true;
        passwordSettings.RequireLowercase = true;
        passwordSettings.AllowUppercase = true;
        passwordSettings.RequireUppercase = true;
        passwordSettings.AllowNumbers = true;
        passwordSettings.RequireNumbers = true;
        passwordSettings.AllowSymbols = true;
        passwordSettings.RequireSymbols = true;

        var password = new PasswordGenerator().Generate(passwordSettings);

        Assert.Contains(password, c => char.IsLower(c));
        Assert.Contains(password, c => char.IsUpper(c));
        Assert.Contains(password, c => char.IsDigit(c));
        Assert.Contains(password, c => passwordSettings.AllowedSpecialCharacters.Contains(c));
    }
}