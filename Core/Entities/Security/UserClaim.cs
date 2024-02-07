using Core.Entities.Common;

namespace Core.Entities.Security;

public class UserClaim : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid ClaimId { get; set; }

    public virtual User User { get; set; }
    public virtual Claim Claim { get; set; }
}
