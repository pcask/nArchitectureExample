using Entity.DTOs.UserClaims;
using Entity.ViewModels.UserClaims;

namespace Core.Abstracts;

public interface IUserClaimService
{
    IEnumerable<UserClaimListVm> GetAll();
    UserClaimVm GetById(Guid id);

    void Add(UserClaimAddDto addUserClaimDto);
    void Update(UserClaimUpdateDto userClaimDto);
    void DeleteById(Guid id);


    Task<IEnumerable<UserClaimListVm>> GetAllAsync();
    Task<UserClaimVm> GetByIdAsync(Guid id);

    Task AddAsync(UserClaimAddDto addUserClaimDto);
    Task UpdateAsync(UserClaimUpdateDto updateUserClaimDto);
    Task DeleteByIdAsync(Guid id);
}