using System.Text;

namespace LocalPasswordGenerator.Models;

public class PasswordGenerator
{
    const string lowercase = "abcdefghijklmnopqrstuvwxyz";
    const string uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    const string numbers = "0123456789";
    const string symbols = "!@#$%^&*()_-+=<>?";

    public string GetDefaultSymbols() 
    {
        return symbols;
    }

    public string Generate(int length, string allowedSpecialChars, bool allowLowercase, bool allowUppercase, bool allowNumbers, bool allowSymbols, 
                                                                    bool requireLowercase, bool requireUppercase, bool requireNumbers, bool requireSymbols) 
    {
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

        do {
            StringBuilder tempPassword = new StringBuilder();
            for (int i = 0; i < length; i++) {
                tempPassword.Append(charPool[random.Next(charPool.Length)]);
            }

            password = tempPassword.ToString();
            attemptCount++;

            // if attempt count is greater than 100, something has gone wrong
            // setting the password to an empty string instead of something like "ERROR" 
            // prevents a less knowledgeable user from using "ERROR" as their password
            if (attemptCount > 100) {
                password = "";
                break;
            }
        }
        while (!IsPasswordValid(password, allowedSpecialChars, requireLowercase, requireUppercase, requireNumbers, requireSymbols));

        return password;
    }


    private bool IsPasswordValid(string password, string allowedSpecialChars, bool needLowercase, bool needUppercase, bool needNumbers, bool needSymbols) 
    {
        bool lowercaseValid = !needLowercase || password.Any(char.IsLower);
        bool uppercaseValid = !needUppercase || password.Any(char.IsUpper);
        bool numbersValid = !needNumbers || password.Any(char.IsDigit);
        bool symbolsValid = !needSymbols || password.Any(c => allowedSpecialChars.Contains(c));

        return lowercaseValid && uppercaseValid && numbersValid && symbolsValid;
    }
}
