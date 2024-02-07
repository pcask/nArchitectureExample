using Core.Entities.Security;

namespace Business.Abstracts;

public interface IClaimService
{
    IEnumerable<Claim> GetAll();
    Claim? GetById(Guid id);

    Claim Add(Claim claim);
    Claim Update(Claim claim);
    void DeleteById(Guid id);


    Task<IEnumerable<Claim>> GetAllAsync();
    Task<Claim?> GetByIdAsync(Guid id);

    Task<Claim> AddAsync(Claim claim);
    Task<Claim> UpdateAsync(Claim claim);
    Task DeleteByIdAsync(Guid id);
}
