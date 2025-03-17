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
    /// <param name="length">The desired length of the password.</param>
    /// <param name="allowedSpecialChars">Specifies which special characters are allowed, as this varies website to website</param>
    /// <param name="allowLowercase">Include lowercase characters in the random pool?</param>
    /// <param name="allowUppercase">Include uppercase characters in the random pool?</param>
    /// <param name="allowNumbers">Include numbers in the random pool?</param>
    /// <param name="allowSymbols">Include special characters in the random pool?</param>
    /// <param name="requireLowercase">Does the password need a lowercase character?</param>
    /// <param name="requireUppercase">Does the password need an uppercase character?</param>
    /// <param name="requireNumbers">Does the password need a number?</param>
    /// <param name="requireSymbols">Does the password need a special character?</param>
    /// <returns>A randomly generated password as a string.</returns>
    public string Generate(int length, string allowedSpecialChars, bool allowLowercase, bool allowUppercase, bool allowNumbers, bool allowSymbols, 
                                                                    bool requireLowercase, bool requireUppercase, bool requireNumbers, bool requireSymbols) 
    {
        // Create a pool of characters out of the allowed character types
        StringBuilder charPool = new StringBuilder();
        if (allowLowercase) 
        {
            charPool.Append(lowercase);
        }
        if (allowUppercase) 
        {
            charPool.Append(uppercase);
        }
        if (allowNumbers) 
        {
            charPool.Append(numbers);
        }
        if (allowSymbols) 
        {
            charPool.Append(allowedSpecialChars);
        }

        Random random = new Random();
        string password = "";
        int attemptCount = 0;

        // Use a do-while loop so that the password generation always happens once
        do {
            StringBuilder tempPassword = new StringBuilder(); // Thousands of string appends could happen here, use StringBuilder.
            for (int i = 0; i < length; i++) {
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
        while (!IsPasswordValid(password, allowedSpecialChars, requireLowercase, requireUppercase, requireNumbers, requireSymbols));

        return password;
    }

    /// <summary>
    /// Checks whether the supplied password meets the supplied criteria or not.
    /// </summary>
    /// <param name="password">The password to check.</param>
    /// <param name="allowedSpecialChars">The string of possible special characters for this password</param>
    /// <param name="needLowercase">Password needs a lowercase character?</param>
    /// <param name="needUppercase">Password needs an uppercase character?</param>
    /// <param name="needNumbers">Password needs a number?</param>
    /// <param name="needSymbols">Password needs a special character from the allowed list?</param>
    /// <returns>'True' if the password passes all tests, 'False' otherwise.</returns>
    private bool IsPasswordValid(string password, string allowedSpecialChars, bool needLowercase, bool needUppercase, bool needNumbers, bool needSymbols) 
    {
        // Check each character set that is required, skip the character sets that aren't
        bool lowercaseValid = !needLowercase || password.Any(char.IsLower);
        bool uppercaseValid = !needUppercase || password.Any(char.IsUpper);
        bool numbersValid = !needNumbers || password.Any(char.IsDigit);
        bool symbolsValid = !needSymbols || password.Any(c => allowedSpecialChars.Contains(c));

        return lowercaseValid && uppercaseValid && numbersValid && symbolsValid;
    }
}
