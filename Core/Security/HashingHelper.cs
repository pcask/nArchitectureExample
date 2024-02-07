using System.Text;
using System.Security.Cryptography;
namespace Core.Security;

public class HashingHelper
{
    public static void CreatePasswordHash
        (string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();

        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    public static async Task<bool> VerifyPasswordHashAsync(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        return await Task.Run(() =>
        {
            using var hmac = new HMACSHA512(passwordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != passwordHash[i])
                {
                    return false;
                }
            }
            return true;
        });
    }
}
