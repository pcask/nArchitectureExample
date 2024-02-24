using Core.Entities.Common;

namespace Core.Entities.Security;

public class Claim : Entity<Guid>
{
    public string Group { get; set; }
    public string Name { get; set; }

    public virtual ICollection<UserClaim> UserClaims { get; set; } = [];
}
