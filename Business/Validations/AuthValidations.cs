using Business.Exceptions;
using Core.Entities.Security;
using Core.Security;
using Core.DTOs.User;

namespace Business.Validations;

public class AuthValidations
{
    public async Task CheckUserAlreadyExistAsync(User? user)
    {
        await Task.Run(() =>
         {
             if (user != null)
                 throw new Exception("User already exist!");
         });
    }
    public void CheckUserExistence(User? user)
    {
        if (user == null) throw new ValidationException("User not found");
    }

    public async Task CheckUserExistenceAsync(User? user)
    {
        await Task.Run(() =>
        {
            if (user == null)
                throw new ValidationException("User not found");
        });
    }

    public async Task CheckNamesAsync(UserRegisterDto userRegisterDto)
    {
        await Task.Run(() =>
        {
            List<string> names = [userRegisterDto.UserName, userRegisterDto.FirstName, userRegisterDto.LastName];

            var name = names.FirstOrDefault(n => n.ToLower().Contains("allah"));
            if (!string.IsNullOrWhiteSpace(name))
                throw new ValidationException($"Username, first or last name must not be {name}");
        });
    }

    public async Task CheckPasswordAsync(string password)
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

    public async Task CheckUserClaimsAsync(User user)
    {
        await Task.Run(() =>
         {
             if (user.UserClaims.Count == 0)
                 throw new ValidationException("User has not any 'Claim'. Please contact to system manager.");
         });

    }
    public async Task ValidatePasswordAsync(User user, string password)
    {
        var result = await HashingHelper.VerifyPasswordHashAsync(password, user.PasswordHash, user.PasswordSalt);
        if (!result)
            throw new ValidationException("Username or password is wrong.");
    }
}
