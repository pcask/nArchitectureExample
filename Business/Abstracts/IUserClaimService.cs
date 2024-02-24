using Entity.DTOs.UserClaims;
using Entity.ViewModels.UserClaims;

namespace Business.Abstracts;

public interface IUserClaimService
{
    IEnumerable<UserClaimListViewModel> GetAll();
    UserClaimViewModel GetById(Guid id);

    void Add(AddUserClaimDto addUserClaimDto);
    void Update(UpdateUserClaimDto userClaimDto);
    void DeleteById(Guid id);


    Task<IEnumerable<UserClaimListViewModel>> GetAllAsync();
    Task<UserClaimViewModel> GetByIdAsync(Guid id);

    Task AddAsync(AddUserClaimDto addUserClaimDto);
    Task UpdateAsync(UpdateUserClaimDto updateUserClaimDto);
    Task DeleteByIdAsync(Guid id);
}