using System.Text;

namespace LocalPasswordGenerator.Models;

/// <summary>
/// Model class that handles the generation of passwords and stores the valid character lists.
/// </summary>
public class PasswordGenerator
{
    const string lowercase = "abcdefghijklmnopqrstuvwxyz";
    const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string numbers = "0123456789";

    /// <summary>
    /// Generates a random password given the provided settings.
    /// </summary>
    /// <param name="settings">The settings that specify the password generation criteria.</param>
    /// <returns>A randomly generated password as a string.</returns>
    public string Generate(PasswordSettings settings) 
    {
        if (!settings.IncludeLowercase && !settings.IncludeUppercase && !settings.IncludeNumbers && !settings.IncludeSymbols) {
            throw new ArgumentException("At least one character type must be included.");
        }

        if (settings.IncludeSymbols && string.IsNullOrEmpty(settings.AllowedSpecialCharacters)) {
            throw new ArgumentException("Allowed special characters cannot be null or empty.");
        }

        // Create a pool of characters out of the allowed character types
        StringBuilder charPool = new StringBuilder();
        if (settings.IncludeLowercase) {
            charPool.Append(lowercase);
        }
        if (settings.IncludeUppercase) {
            charPool.Append(uppercase);
        }
        if (settings.IncludeNumbers) {
            charPool.Append(numbers);
        }
        if (settings.IncludeSymbols) {
            charPool.Append(settings.AllowedSpecialCharacters);
        }

        Random random = new Random();
        string password = "";
        int attemptCount = 0;

        do {
            StringBuilder tempPassword = new StringBuilder(); // StringBuilder is required, thousands of string appends could happen here
            for (int i = 0; i < settings.PasswordLength; i++) {
                tempPassword.Append(charPool[random.Next(charPool.Length)]); 
            }

            password = tempPassword.ToString(); // get the password into the correct format
            attemptCount++;

            /* If attempt count is greater than 100, something has gone wrong, just abort. 
             * Setting the password to an empty string instead of something like "ERROR" 
             * prevents a less tech-savvy user from copying and using "ERROR" as their password. */
            if (attemptCount > 100) {
                password = "";
                break;
            }
        } // If password doesn't meet the criteria, try again. This is more secure than manually editing the password. 
        while (!IsPasswordValid(password, settings));

        return password;
    }

    /// <summary>
    /// Checks whether the supplied password meets the supplied criteria or not.
    /// </summary>
    /// <param name="password">The password to check.</param>
    /// <param name="settings">Settings that specify the criteria to check against.</param>
    /// <returns>'True' if the password passes all tests, 'False' otherwise.</returns>
    private bool IsPasswordValid(string password, PasswordSettings settings) 
    {
        // Check each character set that is required, skip the character sets that aren't
        bool lowercaseValid = !settings.IncludeLowercase || password.Any(char.IsLower);
        bool uppercaseValid = !settings.IncludeUppercase || password.Any(char.IsUpper);
        bool numbersValid = !settings.IncludeNumbers || password.Any(char.IsDigit);
        bool symbolsValid = !settings.IncludeSymbols || password.Any(c => settings.AllowedSpecialCharacters.Contains(c));

        return lowercaseValid && uppercaseValid && numbersValid && symbolsValid;
    }
}
