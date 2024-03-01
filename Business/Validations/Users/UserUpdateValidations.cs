using Core.Exceptions;
using Core.Abstracts;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Entity.DTOs.Users;
using Entity.Entities;
using Business.Validations.Helper;
using Business.Validations.Users;

namespace Core.Validations.Users;

public class UserUpdateValidations(ICheckIdentityService _checkIdentityService, IUserRepository userRepository) : UserValidations(userRepository)
{
    [ValidationMethod(Priority: 0)]
    public async Task CheckUsernameAsync(UserUpdateDto userUpdateDto)
    {
        await ValidationsHelper.CheckNameAsync(userUpdateDto.UserName, "UserName");
    }

    [ValidationMethod(Priority: 0)]
    public async Task CheckEmailAsync(UserUpdateDto userUpdateDto)
    {
        await ValidationsHelper.CheckEmailFormatAsync(userUpdateDto.Email);
    }

    // Override işlemini sadece attribute'e istediğim Priority'i verebilmek için yaptım, Attribute'ün Inhereted özellğini false atadım, aynı attribute çoklu kullanılamıyor!
    [ValidationMethod(Priority: 1)]
    public override async Task CheckExistenceAsync(Guid id)
    {
        await base.CheckExistenceAsync(id);
    }

    [ValidationMethod(Priority: 2)]
    public async Task CheckDatasAsync(UserUpdateDto userUpdateDto)
    {
        User beUpdatedUser = ValidationReturn.Entity;
        // Herhangi bir değişiklik yoksa Db'ye gitmemesi için
        if (beUpdatedUser.UserName == userUpdateDto.UserName && beUpdatedUser.Email == userUpdateDto.Email)
        {
            ValidationReturn.NoNeedToGoToDb = true;
            return;
        }

        var userByUserName = await userRepository.GetAsync(u => u.UserName == userUpdateDto.UserName);
        if (userByUserName != null && userByUserName.Id != beUpdatedUser.Id)
            throw new ValidationException("UserName is already exist!");

        var userByEmail = await userRepository.GetAsync(u => u.Email == userUpdateDto.Email);
        if (userByEmail != null && userByEmail.Id != beUpdatedUser.Id)
            throw new ValidationException("Email is already exist!");
    }
}
