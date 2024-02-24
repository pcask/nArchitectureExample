using Core.Entities.Common;

namespace Core.Entities.Security;

public abstract class AppUser : Entity<Guid>
{
    public string Email { get; set; }
    public virtual ICollection<UserClaim> UserClaims { get; set; } = [];
}