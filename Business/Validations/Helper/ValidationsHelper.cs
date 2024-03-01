using Core.Exceptions;
using System.Net.Mail;

namespace Business.Validations.Helper;

public class ValidationsHelper
{
    private static readonly string[] bannedNames = ["allah", "peygamber", "hz.", "hazreti", "kuran"];

    public static async Task CheckNameAsync(string name, string propertyName)
    {
        await Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException($"{propertyName} cannot be empty");

            if (bannedNames.Any(bn => name.Contains(bn, StringComparison.CurrentCultureIgnoreCase)))
                throw new ValidationException($"{propertyName} cannot be {name}");
        });
    }

    public static async Task CheckEmailFormatAsync(string email)
    {
        await Task.Run(() =>
         {
             if (string.IsNullOrWhiteSpace(email))
                 throw new ValidationException("Email cannot be empty");

             try
             {
                 MailAddress address = new(email);
             }
             catch (Exception)
             {
                 throw new ValidationException("Email is not valid!");
             }
         });
    }

    public static async Task CheckPasswordComplexityAsync(string password)
    {
        await Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                throw new ValidationException("Password must be at least 8 characters.");

            if (!password.Any(isDigit))
                throw new ValidationException("Password requires at least one digit character!");

            if (!password.Any(isLower))
                throw new ValidationException("Password requires at least one lower character!");

            if (!password.Any(isUpper))
                throw new ValidationException("Password requires at least one upper character!");

            if (password.All(isLetterOrDigit))
                throw new ValidationException("Password requires at least one non-alphanumeric character!");
        });

        bool isDigit(char c) => c >= '0' && c <= '9';
        bool isLower(char c) => c >= 'a' && c <= 'z';
        bool isUpper(char c) => c >= 'A' && c <= 'Z';
        bool isLetterOrDigit(char c) => isDigit(c) || isLower(c) || isUpper(c);
    }

}
