using Business.Validations.Common;
using Business.Validations.Helper;
using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions;
using DataAccess.Abstracts;

namespace Business.Validations.Users;

public class UserValidations(IUserRepository userRepository) : ValidationBase
{
    [ValidationMethod(Priority: 0)]
    public virtual async Task CheckExistenceAsync(Guid id)
    {
        ValidationReturn.Entity = await userRepository.GetAsync(u => u.Id == id) ?? throw new ValidationException("User not found!");
    }
}
