namespace Core.DTOs.User;

public class UserRegisterDto
{
    public string IdentificationNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short BirthYear { get; set; }
}
