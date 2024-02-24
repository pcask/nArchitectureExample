using Business.Exceptions;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;

namespace Business.Validations.Users;

public class UserDeleteValidations(IUserRepository userRepository)
{
    [ValidationMethod(Priority: 0)]
    public async Task<ValidationReturn> CheckExistenceAsync(Guid id)
    {
        return new()
        {
            User = await userRepository.GetAsync(u => u.Id == id) ?? throw new ValidationException("User not found")
        };
    }
}
