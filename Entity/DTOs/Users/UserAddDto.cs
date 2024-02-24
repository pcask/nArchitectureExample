using Entity.Entities;

namespace Entity.DTOs.Users;

public class UserAddDto
{
    public string IdentificationNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short BirthYear { get; set; }

    public User GetUser(byte[] passwordHash, byte[] passwordSalt)
    {
        return new()
        {
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IdentificationNumber = IdentificationNumber,
            UserName = UserName,
            Email = Email,
            FirstName = FirstName,
            LastName = LastName,
            BirthYear = BirthYear
        };
    }
}
