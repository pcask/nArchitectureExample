using Business.Abstracts;
using Business.Validations;
using Core.Entities.Security;
using Core.Security;
using Core.Security.JWT;
using Core.DTOs.User;

namespace Business.Concretes;

public class AuthManager(IUserService userService,
                           AuthValidations authValidations,
                           ITokenHelper tokenHelper)
    : IAuthService
{
    public async Task RegisterAsync(UserRegisterDto userRegisterDto)
    {
        await authValidations.CheckPasswordAsync(userRegisterDto.Password);
        await authValidations.CheckNamesAsync(userRegisterDto);

        var _user = await userService.GetByUserNameAsync(userRegisterDto.UserName);

        await authValidations.CheckUserAlreadyExistAsync(_user);

        HashingHelper.CreatePasswordHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        User user = new()
        {
            UserName = userRegisterDto.UserName,
            Email = userRegisterDto.Email,
            FirstName = userRegisterDto.FirstName,
            LastName = userRegisterDto.LastName,
            BirthYear = userRegisterDto.BirthYear,
            IdentificationNumber = userRegisterDto.IdentificationNumber,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await userService.AddAsync(user);
    }
    public async Task<AccessToken> LoginAsync(UserLoginDto userLoginDto)
    {
        var user = await userService.GetByUserNameAsync(userLoginDto.UserName);
        await authValidations.CheckUserExistenceAsync(user);
        await authValidations.CheckPasswordAsync(userLoginDto.Password);
        await authValidations.CheckUserClaimsAsync(user);

        await authValidations.ValidatePasswordAsync(user, userLoginDto.Password);

        return tokenHelper.GenerateAccessToken(user);
    }
}
