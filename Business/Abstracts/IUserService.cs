using Core.Entities;
using Core.Entities.Security;

namespace Business.Abstracts;

public interface IUserService
{
    IEnumerable<User> GetAll();
    User? GetById(Guid id);
    User? GetByUserNameWithClaims(string userName);

    User Add(User user);
    User Update(User user);
    void DeleteById(Guid id);
    CardTransaction AddBalance(CardTransaction cardTransaction);


    #region Async Methods
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUserNameWithClaimsAsync(string userName);


    Task<User> AddAsync(User user);
    Task<User> UpdateAsync(User user);
    Task DeleteByIdAsync(Guid id);
    Task<CardTransaction> AddBalanceAsync(CardTransaction cardTransaction);
    #endregion
}
