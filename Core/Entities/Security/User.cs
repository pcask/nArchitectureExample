using Core.Entities.Common;

namespace Core.Entities.Security;

public class User : Entity<Guid>
{
    public string IdentificationNumber { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public short BirthYear { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }

    public virtual ICollection<UserClaim> UserClaims { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    public virtual ICollection<Card> Cards { get; set; }

    public User()
    {
        UserClaims = new HashSet<UserClaim>();
        Orders = new HashSet<Order>();
        Cards = new HashSet<Card>();
    }
}
