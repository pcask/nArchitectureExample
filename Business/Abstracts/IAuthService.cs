using Core.CrossCuttingConcerns.Validation;
using Core.Security.JWT;
using Entity.DTOs.Users;

namespace Business.Abstracts;

public interface IAuthService
{
    Task RegisterAsync(UserAddDto userRegisterDto);
    Task<AccessToken> LoginAsync(UserLoginDto userLoginDto, ValidationReturn validationReturn = null);
}
