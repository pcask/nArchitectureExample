using Core.Security.JWT;
using Core.DTOs.User;

namespace Business.Abstracts;

public interface IAuthService
{
    Task RegisterAsync(UserRegisterDto userRegisterDto);
    Task<AccessToken> LoginAsync(UserLoginDto userLoginDto);
}
