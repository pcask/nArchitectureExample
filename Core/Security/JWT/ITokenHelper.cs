using Core.Entities.Security;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    AccessToken GenerateAccessToken(AppUser user);
    Task<bool> ValidateTokenAsync(string token);
}
