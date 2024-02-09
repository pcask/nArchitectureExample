using Core.DTOs.UserClaim;
using Core.Entities.Security;

namespace Business.Abstracts;

public interface IUserClaimService
{
    IEnumerable<UserClaim> GetAll();
    UserClaim? GetById(Guid id);

    UserClaim Add(AddUserClaimDto userClaimDto);
    UserClaim Update(UpdateUserClaimDto userClaimDto);
    void DeleteById(Guid id);


    Task<IEnumerable<UserClaim>> GetAllAsync();
    Task<UserClaim?> GetByIdAsync(Guid id);

    Task<UserClaim> AddAsync(AddUserClaimDto userClaimDto);
    Task<UserClaim> UpdateAsync(UpdateUserClaimDto userClaimDto);
    Task DeleteByIdAsync(Guid id);
}