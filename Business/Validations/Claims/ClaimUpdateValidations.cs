using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions;
using DataAccess.Abstracts;
using Entity.DTOs.Claims;

namespace Business.Validations.Claims;

public class ClaimUpdateValidations(IClaimRepository claimRepository) : ClaimValidations(claimRepository)
{
    [ValidationMethod(Priority: 0)]
    public async Task CheckNames(ClaimUpdateDto claimUpdateDto)
    {
        await Task.Run(() =>
                   {
                       if (string.IsNullOrWhiteSpace(claimUpdateDto.Group))
                           throw new ValidationException("Group name of the claim cannot be empty");

                       if (string.IsNullOrWhiteSpace(claimUpdateDto.Name))
                           throw new ValidationException("Name of the claim cannot be empty");
                   });
    }

    [ValidationMethod(Priority: 1)]
    public override async Task CheckExistence(Guid id)
    {
        await base.CheckExistence(id);
    }

    [ValidationMethod(Priority: 2)]
    public async Task CheckDataChanges(ClaimUpdateDto claimUpdateDto)
    {
        await Task.Run(() =>
                   {
                       if (ValidationReturn.Entity.Group == claimUpdateDto.Group && ValidationReturn.Entity.Name == claimUpdateDto.Name)
                           ValidationReturn.NoNeedToGoToDb = true;
                   });
    }
}
