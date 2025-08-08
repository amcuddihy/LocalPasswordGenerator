
using LocalPasswordGenerator.Models;
using System.Globalization;

namespace LocalPasswordGenerator.Services;

public class ZxcvbnPasswordStrengthService : IPasswordStrengthService
{
    public PasswordStrengthResult GetPasswordStrength(string password, int crackSpeedSetting) {
        var result = Zxcvbn.Core.EvaluatePassword(password); // Use the Zxcvbn library to evaluate the password strength 

        var labels = new[] { "Very Weak", "Weak", "Okay", "Strong", "Very Strong" }; // Readable labels for the numeric scores returned by Zxcvbn
        // Get all of the crack time display strings from the Zxcvbn result into an array. This is done to avoid a large switch or if-else block
        var crackTimeResults = new[] { 
            result.CrackTimeDisplay.OfflineFastHashing1e10PerSecond, 
            result.CrackTimeDisplay.OfflineSlowHashing1e4PerSecond,
            result.CrackTimeDisplay.OnlineNoThrottling10PerSecond,
            result.CrackTimeDisplay.OnlineThrottling100PerHour
        };

        var testResult = new PasswordStrengthResult();
        testResult.Score = result.Score; // Set the score from the Zxcvbn result
        testResult.Label = labels[result.Score]; // Set the label based on the score
        
        var textInfo = CultureInfo.CurrentCulture.TextInfo; // Get the current culture's text info for proper title casing of the crack time display
        testResult.CrackTimeDisplay = textInfo.ToTitleCase(crackTimeResults[crackSpeedSetting]); // Set the crack time display based on the selected speed setting
        
        return testResult;
    }
}
