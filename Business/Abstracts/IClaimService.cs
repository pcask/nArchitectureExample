using Core.Entities.Security;
using Entity.DTOs.Claims;
using Entity.ViewModels.Claims;

namespace Core.Abstracts;

public interface IClaimService
{
    IEnumerable<ClaimListVm> GetAll();
    ClaimVm? GetById(Guid id);

    ClaimVm Add(ClaimAddDto claimAddDto);
    void Update(Guid id, ClaimUpdateDto claimUpdateDto);
    void DeleteById(Guid id);


    Task<IEnumerable<ClaimListVm>> GetAllAsync();
    Task<ClaimVm?> GetByIdAsync(Guid id);

    Task<ClaimVm> AddAsync(ClaimAddDto claimAddDto);
    Task UpdateAsync(Guid id, ClaimUpdateDto claimUpdateDto);
    Task DeleteByIdAsync(Guid id);
}
