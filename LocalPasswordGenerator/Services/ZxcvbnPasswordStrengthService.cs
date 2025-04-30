
using LocalPasswordGenerator.Models;

namespace LocalPasswordGenerator.Services;

public class ZxcvbnPasswordStrengthService : IPasswordStrengthService
{
    public PasswordStrengthResult GetPasswordStrength(string password) {

        var result = Zxcvbn.Core.EvaluatePassword(password);    

        var labels = new[] { "Very Weak", "Weak", "Okay", "Strong", "Very Strong" };

        return new PasswordStrengthResult
        {
            Score = result.Score,
            CrackTimeDisplay = result.CrackTimeDisplay.OfflineSlowHashing1e4PerSecond,
            Label = labels[result.Score],
            Suggestions = result.Feedback.Suggestions.ToList(),
        };
    }
}
