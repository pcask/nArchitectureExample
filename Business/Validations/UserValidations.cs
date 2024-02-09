using Autofac.Features.AttributeFilters;
using Business.Exceptions;
using Core.Abstracts;
using Core.Entities.Security;

namespace Business.Validations;

public class UserValidations([KeyFilter("TR")] ICheckIdentityService _checkIdentityService)
{
    //private readonly ICheckIdentityService _checkIdentityService;
    //public UserValidations([FromKeyedServices("TR")] ICheckIdentityService checkIdentityService)
    //{
    //    _checkIdentityService = checkIdentityService;
    //}

   
    public async Task CheckIdentityAsync(User user)
    {
        bool check = await _checkIdentityService.CheckIdentityAsync(user.IdentificationNumber, user.FirstName, user.LastName, user.BirthYear);
        if (!check)
        {
            throw new ValidationException("Identification number is not valid.");
        }
    }

    public void CheckIdentity(User user)
    {
        bool check = _checkIdentityService.CheckIdentity(user.IdentificationNumber, user.FirstName, user.LastName, user.BirthYear);
        if (!check)
        {
            throw new ValidationException("Identification number is not valid.");
        }
    }
    public void CheckNames(User user)
    {
        List<string> names = [user.UserName, user.FirstName, user.LastName];

        var name = names.FirstOrDefault(n => n.ToLower().Contains("allah"));
        if (!string.IsNullOrWhiteSpace(name))
            throw new ValidationException($"Username, first or last name must not be {name}");
    }

    public async Task CheckNamesAsync(User user)
    {
        await Task.Run(() => CheckNames(user));
    }
    public void CheckExistence(User? user)
    {
        if (user == null) throw new ValidationException("User not found");
    }

    public async Task CheckExistenceAsync(User? user)
    {
        await Task.Run(() =>
        {
            if (user == null)
                throw new ValidationException("User not found");
        });
    }
}
