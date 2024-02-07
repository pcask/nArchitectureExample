using Business.Exceptions;
using Core.Entities.Security;

namespace Business.Validations;

public class ClaimValidations
{
    public void CheckExistence(Claim? claim)
    {
        if (claim == null) throw new ValidationException("Claim not found");
    }

    public async Task CheckExistenceAsync(Claim? claim)
    {
        await Task.Run(() =>
        {
            if (claim == null)
                throw new ValidationException("Claim not found");
        });
    }
}