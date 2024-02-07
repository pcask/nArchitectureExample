using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Text;
using SSC = System.Security.Claims;
using Core.Entities.Security;

namespace Core.Security.JWT;

public class JWTTokenHelper : ITokenHelper
{
    private readonly TokenOptions tokenOptions;
    private readonly SymmetricSecurityKey symmetricSecurityKey;

    public JWTTokenHelper(IConfiguration Configuration)
    {
        tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>() ?? throw new Exception("Token options cannot be read!");
        symmetricSecurityKey = new(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey));
    }

    public AccessToken GenerateAccessToken(User user)
    {
        var claims = user.UserClaims
             .Select(uc => $"{uc.Claim.Group}.{uc.Claim.Name}")
             .Select(cName => new SSC.Claim(SSC.ClaimTypes.Role, cName))
             .ToList();

        claims.Add(new(SSC.ClaimTypes.Email, user.Email.ToString()));

        SigningCredentials signingCredentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

        var expirationDate = DateTime.UtcNow.AddMinutes(tokenOptions.ExpirationMinute);

        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = tokenOptions.Issuer,
            Audience = tokenOptions.Audience,
            Expires = expirationDate,
            NotBefore = DateTime.UtcNow, // Token şu andan itabaren geçerli olsun demiş oluyoruz.
            Subject = new SSC.ClaimsIdentity(claims), // Claims property'si Dictionary tipinden ve çoklu key eklenemeyeceği için subject'i kullandım.
            SigningCredentials = signingCredentials
        };

        string token = new JsonWebTokenHandler().CreateToken(securityTokenDescriptor);

        return new AccessToken { ExpirationDate = expirationDate, Token = token };
    }

    public async Task<bool> ValidateTokenAsync(string token)
    {
        JsonWebTokenHandler handler = new();
        var result = await handler.ValidateTokenAsync(token, new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,

            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null && expires > DateTime.UtcNow,
            IssuerSigningKey = symmetricSecurityKey
        });

        return result.IsValid;
    }
}
