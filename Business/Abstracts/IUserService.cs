using Entity.DTOs.Users;
using Entity.ViewModels.Users;

namespace Core.Abstracts;

public interface IUserService
{
    IEnumerable<UserListVm> GetAll();
    UserVm GetById(Guid id);
    UserVm GetByUserName(string userName);
    UserVm GetByEmail(string email);

    void Add(UserAddDto userAddDto);
    void Update(Guid id, UserUpdateDto userUpdateDto);
    void DeleteById(Guid id);

    #region Async Methods
    Task<IEnumerable<UserListVm>> GetAllAsync();
    Task<UserVm> GetByIdAsync(Guid id);
    Task<UserVm> GetByUserNameAsync(string userName);
    Task<UserVm> GetByEmailAsync(string email);

    Task AddAsync(UserAddDto userAddDto);
    Task UpdateAsync(Guid id, UserUpdateDto userUpdateDto);
    Task DeleteByIdAsync(Guid id);
    #endregion
}
