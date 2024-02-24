using Core.Entities.Security;

namespace Entity.ViewModels.UserClaims;

public class UserClaimViewModel
{
    public Guid UserId { get; set; }
    public Guid ClaimId { get; set; }

    public static UserClaimViewModel GetModel(UserClaim userClaim)
    {
        return new()
        {
            UserId = userClaim.AppUserId,
            ClaimId = userClaim.ClaimId
        };
    }
}