using Business.Exceptions;
using Business.Validations.Helper;
using Core.Abstracts;
using Core.CrossCuttingConcerns.Validation;
using DataAccess.Abstracts;
using Entity.DTOs.Users;

namespace Business.Validations.Users
{
    // Autofac Keyed Service' in çözümlenmesi için KeyFilter kullanımı;

    //public class UserAddValidations([KeyFilter("TR")] ICheckIdentityService checkIdentityService)
    //{
    //    
    //}

    public class UserAddValidations(ICheckIdentityService _checkIdentityService, IUserRepository userRepository)
    {
        [ValidationMethod(Priority: 0)]
        public async Task CheckNamesAsync(UserAddDto userAddDto)
        {
            List<(string name, string propertyName)> names = [(userAddDto.UserName, "UserName"), (userAddDto.FirstName, "FirstName"), (userAddDto.LastName, "LastName")];

            names.ForEach(async n => await ValidationsHelper.CheckNameAsync(n.name, n.propertyName));
        }

        [ValidationMethod(Priority: 1)]
        public async Task CheckEmailFormatAsync(UserAddDto userAddDto)
        {
            await ValidationsHelper.CheckEmailFormatAsync(userAddDto.Email);
        }

        [ValidationMethod(Priority: 2)]
        public async Task CheckPasswordAsync(UserAddDto userAddDto)
        {
            await ValidationsHelper.CheckPasswordComplexityAsync(userAddDto.Password);
        }

        [ValidationMethod(Priority: 3)]
        public async Task CheckIfUserNameExistsAsync(UserAddDto userAddDto)
        {
            if (await userRepository.GetAsync(u => u.UserName == userAddDto.UserName) != null)
                throw new ValidationException("UserName is already exist!");
        }

        [ValidationMethod(Priority: 3)]
        public async Task CheckIfEmailExistsAsync(UserAddDto userAddDto)
        {
            if (await userRepository.GetAsync(u => u.Email == userAddDto.Email) != null)
                throw new ValidationException("Email is already exist!");
        }

        [ValidationMethod(Priority: 4)]
        public async Task CheckIdentityAsync(UserAddDto userAddDto)
        {
            bool check = await _checkIdentityService.CheckIdentityAsync(userAddDto.IdentificationNumber, userAddDto.FirstName, userAddDto.LastName, userAddDto.BirthYear);
            if (!check)
                throw new ValidationException("Identity is not valid.");
        }
    }
}
