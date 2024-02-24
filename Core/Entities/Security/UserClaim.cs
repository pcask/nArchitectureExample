using Core.Entities.Common;

namespace Core.Entities.Security;

public class UserClaim : Entity<Guid>
{
    public Guid AppUserId { get; set; }
    public Guid ClaimId { get; set; }

    public virtual AppUser User { get; set; }
    public virtual Claim Claim { get; set; }
}
