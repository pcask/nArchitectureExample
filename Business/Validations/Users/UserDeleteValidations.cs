using Core.Exceptions;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Business.Validations.Common;
using Business.Validations.Users;

namespace Core.Validations.Users;

public class UserDeleteValidations(IUserRepository userRepository) : UserValidations(userRepository)
{
    [ValidationMethod(Priority: 0)]
    public override async Task CheckExistenceAsync(Guid id)
    {
        await base.CheckExistenceAsync(id);
    }
}