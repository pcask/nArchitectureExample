using Core.Abstracts;
using DataAccess.Abstracts;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Entity.DTOs.Users;
using Core.Validations.Users;
using Entity.ViewModels.Users;
using Core.Security;
using Core.Aspects.Autofac.Security;
using Core.CrossCuttingConcerns.Security;
using Business.Concretes.Common;
using Business.Validations.Users;
using Business.Validations.Helper;

namespace Core.Concretes;

[MustBeAuthorized]
public class UserManager(IUserRepository userRepository,
                         ICardTransactionService cardTransactionService)
    : ManagerBase, IUserService
{

    [CacheRemoveAspect]
    [SecurityAspect]
    [ValidationAspect(typeof(UserAddValidations))]
    public void Add(UserAddDto userAddDto)
    {
        HashingHelper.CreatePasswordHash(userAddDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        userRepository.Add(userAddDto.GetEntity(passwordHash, passwordSalt));
    }

    [CacheRemoveAspect]
    [SecurityAspect]
    [ValidationAspect(typeof(UserAddValidations))]
    public async Task AddAsync(UserAddDto userAddDto)
    {
        HashingHelper.CreatePasswordHash(userAddDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        await userRepository.AddAsync(userAddDto.GetEntity(passwordHash, passwordSalt));
    }


    [CacheRemoveAspect]
    [SecurityAspect]
    [ValidationAspect(typeof(UserDeleteValidations))]
    public void DeleteById(Guid id)
    {
        userRepository.Delete(ValidationReturn.Entity);
    }

    [CacheRemoveAspect]
    [SecurityAspect]
    [ValidationAspect(typeof(UserDeleteValidations))]
    public async Task DeleteByIdAsync(Guid id)
    {
        await userRepository.DeleteAsync(ValidationReturn.Entity);
    }

    [SecurityAspect]
    [ValidationAspect(typeof(UserValidations))]
    public UserVm GetById(Guid id)
    {
        return UserVm.GetModel(ValidationReturn.Entity);
    }

    [CacheAspect(10)]
    [SecurityAspect]
    [ValidationAspect(typeof(UserValidations))]
    public async Task<UserVm> GetByIdAsync(Guid id)
    {
        return await Task.Run(() => UserVm.GetModel(ValidationReturn.Entity));
    }

    [CacheAspect(10)]
    [SecurityAspect]
    public IEnumerable<UserListVm> GetAll()
    {
        return UserListVm.GetModels(userRepository.GetAll());
    }

    [CacheAspect(10, Priority = 1)]
    [SecurityAspect]
    [PerformanceAspect(1, Priority = 2)]
    public async Task<IEnumerable<UserListVm>> GetAllAsync()
    {
        return UserListVm.GetModels(await userRepository.GetAllAsync());
    }

    [CacheRemoveAspect]
    [SecurityAspect]
    [ValidationAspect(typeof(UserUpdateValidations))]
    public void Update(Guid id, UserUpdateDto userUpdateDto)
    {
        if (ValidationReturn.NoNeedToGoToDb)
            return;

        userRepository.Update(userUpdateDto.GetEntity(ValidationReturn.Entity));
    }

    [CacheRemoveAspect]
    [SecurityAspect]
    [ValidationAspect(typeof(UserUpdateValidations))]
    public async Task UpdateAsync(Guid id, UserUpdateDto userUpdateDto)
    {
        if (ValidationReturn.NoNeedToGoToDb)
            return;

        await userRepository.UpdateAsync(userUpdateDto.GetEntity(ValidationReturn.Entity));
    }


    [SecurityAspect]
    [ValidationAspect(typeof(UserValidations))]
    public UserVm GetByUserName(string userName)
    {
        ValidationsHelper.CheckNameAsync(userName, "UserName").GetAwaiter().GetResult();

        var user = userRepository.Get(u => u.UserName == userName);
        return UserVm.GetModel(user);
    }

    [SecurityAspect]
    public async Task<UserVm> GetByUserNameAsync(string userName)
    {
        await ValidationsHelper.CheckNameAsync(userName, "UserName");
        var user = await userRepository.GetAsync(u => u.UserName == userName);
        return UserVm.GetModel(user);
    }

    [SecurityAspect]
    public UserVm GetByEmail(string email)
    {
        ValidationsHelper.CheckEmailFormatAsync(email).GetAwaiter().GetResult();

        var user = userRepository.Get(u => u.Email == email);
        return UserVm.GetModel(user);
    }

    [SecurityAspect]
    [ValidationAspect(typeof(UserValidations))]
    public async Task<UserVm> GetByEmailAsync(string email)
    {
        await ValidationsHelper.CheckEmailFormatAsync(email);
        var user = await userRepository.GetAsync(u => u.Email == email);
        return UserVm.GetModel(user);
    }
}
