using Business.Exceptions;
using Core.Entities.Security;

namespace Business.Validations;

public class UserClaimValidations
{
    public void CheckExistence(UserClaim? userClaim)
    {
        if (userClaim == null) throw new ValidationException("UserClaim not found");
    }

    public async Task CheckExistenceAsync(UserClaim? userClaim)
    {
        await Task.Run(() =>
        {
            if (userClaim == null)
                throw new ValidationException("UserClaim not found");
        });
    }
}
