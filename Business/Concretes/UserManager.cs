using Business.Abstracts;
using Entity.Entities;
using DataAccess.Abstracts;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Caching;
using Entity.DTOs.CardTransactions;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Entity.DTOs.Users;
using Business.Validations.Users;
using Entity.ViewModels.Users;
using Core.Security;

namespace Business.Concretes;

public class UserManager(IUserRepository userRepository,
                         ICardTransactionService cardTransactionService)
    : IUserService
{


    [CacheRemoveAspect]
    [ValidationAspect(typeof(UserAddValidations))]
    public void Add(UserAddDto userAddDto)
    {
        HashingHelper.CreatePasswordHash(userAddDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        userRepository.Add(userAddDto.GetUser(passwordHash, passwordSalt));
    }

    [CacheRemoveAspect]
    [ValidationAspect(typeof(UserAddValidations))]
    public async Task AddAsync(UserAddDto userAddDto)
    {
        HashingHelper.CreatePasswordHash(userAddDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

        await userRepository.AddAsync(userAddDto.GetUser(passwordHash, passwordSalt));
    }

    public void AddBalance(AddCardTransactionDto addCardTransactionDto)
    {
        CardTransaction cardTransaction = new()
        {
            CardId = addCardTransactionDto.CardId,
            CreatedDate = DateTime.UtcNow,
            Balance = addCardTransactionDto.Balance,
        };

        cardTransactionService.Add(cardTransaction);
    }

    public async Task AddBalanceAsync(AddCardTransactionDto addCardTransactionDto)
    {
        CardTransaction cardTransaction = new()
        {
            CardId = addCardTransactionDto.CardId,
            CreatedDate = DateTime.UtcNow,
            Balance = addCardTransactionDto.Balance,
        };
        await cardTransactionService.AddAsync(cardTransaction);
    }


    [CacheRemoveAspect]
    [ValidationAspect(typeof(UserDeleteValidations))]
    public void DeleteById(Guid id, ValidationReturn vr)
    {
        User beDeletedUser = vr?.User != null ? vr.User as User : userRepository.Get(u => u.Id == id);

        userRepository.Delete(beDeletedUser);
    }

    [CacheRemoveAspect]
    [ValidationAspect(typeof(UserDeleteValidations))]
    public async Task DeleteByIdAsync(Guid id, ValidationReturn vr)
    {
        User beDeletedUser = vr?.User != null ? vr.User as User : await userRepository.GetAsync(u => u.Id == id);

        await userRepository.DeleteAsync(beDeletedUser);
    }

    public UserViewModel GetById(Guid id)
    {
        var user = userRepository.Get(u => u.Id == id);
        return UserViewModel.GetModel(user);
    }
    [CacheAspect(10)]
    public async Task<UserViewModel> GetByIdAsync(Guid id)
    {
        var user = await userRepository.GetAsync(u => u.Id == id);
        return UserViewModel.GetModel(user);
    }


    [CacheAspect(10)]
    public IEnumerable<UserListViewModel> GetAll()
    {
        var users = userRepository.GetAll();

        return UserListViewModel.GetModels(users);
    }

    [CacheAspect(10, Priority = 1)]
    [PerformanceAspect(1, Priority = 2)]
    public async Task<IEnumerable<UserListViewModel>> GetAllAsync()
    {
        var users = await userRepository.GetAllAsync();

        return UserListViewModel.GetModels(users);
    }

    [CacheRemoveAspect]
    [ValidationAspect(typeof(UserUpdateValidations))]
    public void Update(Guid id, UserUpdateDto userUpdateDto, ValidationReturn vr)
    {
        if (vr.NoNeedToGoToDb)
            return;

        userRepository.Update(userUpdateDto.GetUser(vr.User as User));
    }

    [CacheRemoveAspect]
    [ValidationAspect(typeof(UserUpdateValidations))]
    public async Task UpdateAsync(Guid id, UserUpdateDto userUpdateDto, ValidationReturn vr)
    {
        // Tüm bu validation işlemlerini artık ValidationAspect ile yönetiyoruz artık.
        //await userValidations.CheckIdentityAsync(user);
        //await userValidations.CheckNamesAsync(user);
        //var _user = await userRepository.GetAsync(u => u.Id == user.Id);
        //await userValidations.CheckExistenceAsync(_user);

        if (vr.NoNeedToGoToDb)
            return;

        await userRepository.UpdateAsync(userUpdateDto.GetUser(vr.User as User));
    }

    public UserViewModel GetByUserName(string userName)
    {
        var user = userRepository.Get(u => u.UserName == userName);
        return UserViewModel.GetModel(user);
    }

    public async Task<UserViewModel> GetByUserNameAsync(string userName)
    {
        var user = await userRepository.GetAsync(u => u.UserName == userName);
        return UserViewModel.GetModel(user);
    }

    public UserViewModel GetByEmail(string email)
    {
        var user = userRepository.Get(u => u.Email == email);
        return UserViewModel.GetModel(user);
    }

    public async Task<UserViewModel> GetByEmailAsync(string email)
    {
        var user = await userRepository.GetAsync(u => u.Email == email);
        return UserViewModel.GetModel(user);
    }
}
