using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;

namespace Business.Validations.Claims;

public class ClaimDeleteValidations(IClaimRepository claimRepository) : ClaimValidations(claimRepository)
{
    [ValidationMethod(Priority: 0)]
    public override async Task CheckExistence(Guid id)
    {
        await base.CheckExistence(id);
    }
}