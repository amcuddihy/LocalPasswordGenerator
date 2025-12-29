using LocalPasswordGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalPasswordGenerator.Services;

public class SimplePasswordGeneratorService : IPasswordGeneratorService 
{
    const string lowercase = "abcdefghijklmnopqrstuvwxyz";
    const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string numbers = "0123456789";

    public string GeneratePassword(PasswordSettings settings) {
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

            password = tempPassword.ToString();
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

    private bool IsPasswordValid(string password, PasswordSettings settings) {
        bool lowercaseValid = true;
        if (settings.IncludeLowercase) {
            lowercaseValid = password.Any(char.IsLower);
        }

        bool uppercaseValid = true;
        if (settings.IncludeUppercase) { 
            uppercaseValid  = password.Any(char.IsUpper); 
        }

        bool numbersValid = true;
        if (settings.IncludeNumbers) { 
            numbersValid = password.Any(char.IsDigit);
        }

        bool symbolsValid = true; 
        if (settings.IncludeSymbols) { 
            symbolsValid = password.Any(c => settings.AllowedSpecialCharacters.Contains(c)); 
        }

        return lowercaseValid && uppercaseValid && numbersValid && symbolsValid;
    }
}
