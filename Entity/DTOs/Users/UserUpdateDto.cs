using Entity.Entities;

namespace Entity.DTOs.Users;

public class UserUpdateDto
{
    public string UserName { get; set; }
    public string Email { get; set; }

    public User GetEntity(User beUpdatedUser)
    {
        beUpdatedUser.UserName = UserName;
        beUpdatedUser.Email = Email;

        return beUpdatedUser;
    }
}