using Core.Entities.Security;

namespace Business.Abstracts;

public interface IUserClaimService
{
    IEnumerable<UserClaim> GetAll();
    UserClaim? GetById(Guid id);

    UserClaim Add(UserClaim userClaim);
    UserClaim Update(UserClaim userClaim);
    void DeleteById(Guid id);


    Task<IEnumerable<UserClaim>> GetAllAsync();
    Task<UserClaim?> GetByIdAsync(Guid id);

    Task<UserClaim> AddAsync(UserClaim userClaim);
    Task<UserClaim> UpdateAsync(UserClaim userClaim);
    Task DeleteByIdAsync(Guid id);
}