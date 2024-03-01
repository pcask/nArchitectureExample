using Business.Validations.Common;
using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions;
using DataAccess.Abstracts;

namespace Business.Validations.Claims;

public class ClaimValidations(IClaimRepository claimRepository) : ValidationBase
{
    [ValidationMethod(Priority: 0)]
    public virtual async Task CheckExistence(Guid id)
    {
        ValidationReturn.Entity = await claimRepository.GetAsync(c => c.Id == id) ?? throw new ValidationException("Claim was not found!");
    }
}
