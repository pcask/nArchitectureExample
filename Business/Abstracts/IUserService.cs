using Core.CrossCuttingConcerns.Validation;
using Entity.DTOs.CardTransactions;
using Entity.DTOs.Users;
using Entity.ViewModels.Users;

namespace Business.Abstracts;

public interface IUserService
{
    IEnumerable<UserListViewModel> GetAll();
    UserViewModel GetById(Guid id);
    UserViewModel GetByUserName(string userName);
    UserViewModel GetByEmail(string email);

    void Add(UserAddDto userAddDto);
    void Update(Guid id, UserUpdateDto userUpdateDto, ValidationReturn validationReturn = null);
    void DeleteById(Guid id, ValidationReturn validationReturn = null);
    void AddBalance(AddCardTransactionDto addCardTransactionDto);


    #region Async Methods
    Task<IEnumerable<UserListViewModel>> GetAllAsync();
    Task<UserViewModel> GetByIdAsync(Guid id);
    Task<UserViewModel> GetByUserNameAsync(string userName);
    Task<UserViewModel> GetByEmailAsync(string email);

    Task AddAsync(UserAddDto userAddDto);
    Task UpdateAsync(Guid id, UserUpdateDto userUpdateDto, ValidationReturn vr = null);
    Task DeleteByIdAsync(Guid id, ValidationReturn validationReturn = null);
    Task AddBalanceAsync(AddCardTransactionDto addCardTransactionDto);
    #endregion
}
