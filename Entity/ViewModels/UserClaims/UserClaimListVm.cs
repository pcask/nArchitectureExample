using Core.Entities.Security;

namespace Entity.ViewModels.UserClaims;

public class UserClaimListVm
{
    public Guid UserId { get; set; }
    public Guid ClaimId { get; set; }

    public static IEnumerable<UserClaimListVm> GetModels(IEnumerable<UserClaim> userClaims)
    {
        return userClaims.Select(uc => new UserClaimListVm()
        {
            UserId = uc.AppUserId,
            ClaimId = uc.ClaimId
        });
    }
}
