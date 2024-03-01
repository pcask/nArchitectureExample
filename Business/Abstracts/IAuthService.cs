using Core.Security.JWT;
using Entity.DTOs.Users;

namespace Core.Abstracts;

public interface IAuthService
{
    Task RegisterAsync(UserAddDto userRegisterDto);
    Task<AccessToken> LoginAsync(UserLoginDto userLoginDto);
}
