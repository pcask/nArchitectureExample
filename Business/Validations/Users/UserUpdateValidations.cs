using Business.Exceptions;
using Business.Validations.Helper;
using Core.Abstracts;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Entity.DTOs.Users;
using Entity.Entities;

namespace Business.Validations.Users;

public class UserUpdateValidations(ICheckIdentityService _checkIdentityService, IUserRepository userRepository)
{
    private User beUpdatedUser;

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


    [ValidationMethod(Priority: 1)]
    public async Task CheckExistenceAsync(Guid id)
    {
        beUpdatedUser = await userRepository.GetAsync(u => u.Id == id) ?? throw new ValidationException("User not found");
    }

    [ValidationMethod(Priority: 2)]
    public async Task<ValidationReturn> CheckDatasAsync(UserUpdateDto userUpdateDto)
    {
        ValidationReturn vr = new() { User = beUpdatedUser };

        // Herhangi bir değişiklik yoksa Db'ye gitmemesi için
        if (beUpdatedUser.UserName == userUpdateDto.UserName && beUpdatedUser.Email == userUpdateDto.Email)
        {
            vr.NoNeedToGoToDb = true;
            return vr;
        }

        var userByUserName = await userRepository.GetAsync(u => u.UserName == userUpdateDto.UserName);
        if (userByUserName != null && userByUserName.Id != beUpdatedUser.Id)
            throw new ValidationException("UserName is already exist!");

        var userByEmail = await userRepository.GetAsync(u => u.Email == userUpdateDto.Email);
        if (userByEmail != null && userByEmail.Id != beUpdatedUser.Id)
            throw new ValidationException("Email is already exist!");

        return vr;
    }
}
