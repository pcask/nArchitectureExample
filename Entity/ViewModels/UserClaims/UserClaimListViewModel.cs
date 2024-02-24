using Core.Entities.Security;

namespace Entity.ViewModels.UserClaims;

public class UserClaimListViewModel
{
    public Guid UserId { get; set; }
    public Guid ClaimId { get; set; }

    public static IEnumerable<UserClaimListViewModel> GetModels(IEnumerable<UserClaim> userClaims)
    {
        return userClaims.Select(uc => new UserClaimListViewModel()
        {
            UserId = uc.AppUserId,
            ClaimId = uc.ClaimId
        });
    }
}
