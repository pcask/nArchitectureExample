using Business.Abstracts;
using Business.Validations;
using Core.Entities;
using Core.Entities.Security;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class UserManager(IUserRepository userRepository,
                         UserValidations userValidations,
                         ICardTransactionService cardTransactionService)
    : IUserService
{
    public User Add(User user)
    {
        userValidations.CheckIdentity(user);
        userValidations.CheckNames(user);
        return userRepository.Add(user);
    }

    public async Task<User> AddAsync(User user)
    {
        await userValidations.CheckIdentityAsync(user);
        await userValidations.CheckNamesAsync(user);
        return await userRepository.AddAsync(user);
    }

    public CardTransaction AddBalance(CardTransaction cardTransaction)
    {
        return cardTransactionService.Add(cardTransaction);
    }

    public async Task<CardTransaction> AddBalanceAsync(CardTransaction cardTransaction)
    {
        return await cardTransactionService.AddAsync(cardTransaction);
    }

    public void DeleteById(Guid id)
    {
        var user = userRepository.Get(u => u.Id == id);
        userValidations.CheckExistence(user);

        userRepository.Delete(user);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var user = userRepository.Get(u => u.Id == id);
        await userValidations.CheckExistenceAsync(user);

        await userRepository.DeleteAsync(user);
    }

    public User? GetById(Guid id)
    {
        return userRepository.Get(u => u.Id == id);
    }
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await userRepository.GetAsync(u => u.Id == id);
    }

    public User? GetByUserName(string userName)
    {
        return userRepository.Get(u => u.UserName == userName);
    }

    public async Task<User?> GetByUserNameAsync(string userName)
    {
        return await userRepository.GetAsync(u => u.UserName == userName);
    }

    public IEnumerable<User> GetAll() => userRepository.GetAll();

    public async Task<IEnumerable<User>> GetAllAsync() => await userRepository.GetAllAsync();

    public User Update(User user)
    {
        userValidations.CheckIdentity(user);
        userValidations.CheckNames(user);

        var _user = userRepository.Get(u => u.Id == user.Id);
        userValidations.CheckExistence(_user);

        return userRepository.Update(user);
    }

    public async Task<User> UpdateAsync(User user)
    {
        await userValidations.CheckIdentityAsync(user);
        await userValidations.CheckNamesAsync(user);

        var _user = await userRepository.GetAsync(u => u.Id == user.Id);
        await userValidations.CheckExistenceAsync(_user);

        return await userRepository.UpdateAsync(user);
    }
}
