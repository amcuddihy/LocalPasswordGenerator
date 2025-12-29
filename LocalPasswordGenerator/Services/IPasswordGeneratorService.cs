using LocalPasswordGenerator.Models;

namespace LocalPasswordGenerator.Services;

public interface IPasswordGeneratorService
{
    public string GeneratePassword(PasswordSettings settings);
}
