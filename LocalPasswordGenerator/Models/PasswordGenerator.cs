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
    const string symbols = "!@#$%^&*()_-+=<>?";

    /// <summary>
    /// Returns the list of default symbols. Placing it here instead of the viewModel
    /// to keep all of the business logic in the models.
    /// </summary>
    /// <returns>A string containing every default special character.</returns>
    public string GetDefaultSymbols() 
    {
        return symbols;
    }

    /// <summary>
    /// Generates a random password given the provided settings.
    /// </summary>
    /// <param name="settings">The settings that specify the password generation criteria.</param>
    /// <returns>A randomly generated password as a string.</returns>
    public string Generate(PasswordSettings settings) 
    {
        if (!settings.AllowLowercase && !settings.AllowUppercase && !settings.AllowNumbers && !settings.AllowSymbols) {
            throw new ArgumentException("At least one character type must be selected.");
        }

        if (string.IsNullOrEmpty(settings.AllowedSpecialCharacters)) {
            throw new ArgumentException("Allowed special characters cannot be null or empty.");
        }

        // Create a pool of characters out of the allowed character types
        StringBuilder charPool = new StringBuilder();
        if (settings.AllowLowercase) {
            charPool.Append(lowercase);
        }
        if (settings.AllowUppercase) {
            charPool.Append(uppercase);
        }
        if (settings.AllowNumbers) {
            charPool.Append(numbers);
        }
        if (settings.AllowSymbols) {
            charPool.Append(settings.AllowedSpecialCharacters);
        }

        Random random = new Random();
        string password = "";
        int attemptCount = 0;

        // Use a do-while loop so that the password generation always happens once
        do {
            StringBuilder tempPassword = new StringBuilder(); // Thousands of string appends could happen here, use StringBuilder.
            for (int i = 0; i < settings.PasswordLength; i++) {
                tempPassword.Append(charPool[random.Next(charPool.Length)]); // grab a random character from the charPool and append it to the password
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
        } // Keep looping and generating passwords until a password is valid. Swapping characters would reduce the password security.
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
        bool lowercaseValid = !settings.RequireLowercase || password.Any(char.IsLower);
        bool uppercaseValid = !settings.RequireUppercase || password.Any(char.IsUpper);
        bool numbersValid = !settings.RequireNumbers || password.Any(char.IsDigit);
        bool symbolsValid = !settings.RequireSymbols || password.Any(c => settings.AllowedSpecialCharacters.Contains(c));

        return lowercaseValid && uppercaseValid && numbersValid && symbolsValid;
    }
}
