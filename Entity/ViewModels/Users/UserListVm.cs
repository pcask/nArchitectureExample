using Entity.Entities;

namespace Entity.ViewModels.Users;

public class UserListVm
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public static IEnumerable<UserListVm> GetModels(IEnumerable<User> users)
    {
        return users.Select(u => new UserListVm()
        {
            Id = u.Id,
            Email = u.Email,
            UserName = u.UserName,
            FirstName = u.FirstName,
            LastName = u.LastName
        });
    }
}
