using LocalPasswordGenerator.Models;

namespace LocalPasswordGenerator.Services;

public interface IPasswordStrengthService
{
    PasswordStrengthResult GetPasswordStrength(string password, int crackSpeedSetting);
}
