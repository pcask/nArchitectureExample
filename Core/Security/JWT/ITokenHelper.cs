using Core.Entities.Security;

namespace Core.Security.JWT;

public interface ITokenHelper
{
    AccessToken GenerateAccessToken(User user);
    Task<bool> ValidateTokenAsync(string token);
}
