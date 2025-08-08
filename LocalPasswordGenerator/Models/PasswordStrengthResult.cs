
namespace LocalPasswordGenerator.Models;

public class PasswordStrengthResult 
{
    public int Score { get; set; } // 0-4
    public string CrackTimeDisplay { get; set; } = string.Empty;
    public string Label { get; set; } = string.Empty;
}