using Business.Exceptions;
using Business.Validations.Helper;
using Core.CrossCuttingConcerns.Validation;
using Core.Security;
using DataAccess.Abstracts;
using Entity.DTOs.Users;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Validations.Auths
{
    public class LoginValidations(IUserRepository userRepository)
    {
        User beLoggedInUser;

        [ValidationMethod(Priority: 0)]
        public async Task CheckPasswordComplexityAsync(UserLoginDto loginDto)
        {
            await ValidationsHelper.CheckPasswordComplexityAsync(loginDto.Password);
        }

        [ValidationMethod(Priority: 1)]
        public async Task CheckUserNameExistenceAsync(UserLoginDto loginDto)
        {
            beLoggedInUser = await userRepository.GetAsync(u => u.UserName == loginDto.UserName,
                                                           qU => qU.Include(u => u.UserClaims).ThenInclude(uc => uc.Claim))
                                                           ?? throw new ValidationException("UserName or password is wrong!");
        }

        [ValidationMethod(Priority: 2)]
        public async Task CheckIfUserHasAnyClaimsAsync()
        {
            if (beLoggedInUser.UserClaims.Count == 0)
                throw new ValidationException("User has not any 'Claim'. Please contact to system manager.");
        }

        [ValidationMethod(Priority: 3)]
        public async Task<ValidationReturn> ValidatePasswordAsync(UserLoginDto loginDto)
        {
            var result = await HashingHelper.VerifyPasswordHashAsync(loginDto.Password, beLoggedInUser.PasswordHash, beLoggedInUser.PasswordSalt);
            if (!result)
                throw new ValidationException("Username or password is wrong!");

            return new() { User = beLoggedInUser };
        }
    }
}
