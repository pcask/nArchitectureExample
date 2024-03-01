using Core.Entities.Security;

namespace Entity.DTOs.UserClaims;

public class UserClaimAddDto
{
    public Guid UserId { get; set; }
    public Guid ClaimId { get; set; }

    public UserClaim GetEntity()
    {
        return new UserClaim()
        {
            AppUserId = UserId,
            ClaimId = ClaimId
        };
    }
}
