using Business.Abstracts;
using Business.Validations;
using Core.Entities.Security;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class ClaimManager(IClaimRepository claimRepository, ClaimValidations claimValidations) : IClaimService
{
    public Claim Add(Claim claim)
    {
        return claimRepository.Add(claim);
    }

    public async Task<Claim> AddAsync(Claim claim)
    {
        return await claimRepository.AddAsync(claim);
    }

    public void DeleteById(Guid id)
    {
        var claim = claimRepository.Get(c => c.Id == id);

        claimValidations.CheckExistence(claim);
        claimRepository.Delete(claim);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var claim = await claimRepository.GetAsync(c => c.Id == id);

        await claimValidations.CheckExistenceAsync(claim);
        await claimRepository.DeleteAsync(claim);
    }

    public IEnumerable<Claim> GetAll() => claimRepository.GetAll();

    public async Task<IEnumerable<Claim>> GetAllAsync() => await claimRepository.GetAllAsync();

    public Claim? GetById(Guid id) => claimRepository.Get(c => c.Id == id);

    public async Task<Claim?> GetByIdAsync(Guid id) => await claimRepository.GetAsync(c => c.Id == id);

    public Claim Update(Claim claim)
    {
        var _claim = claimRepository.Get(c => c.Id == claim.Id);
        claimValidations.CheckExistence(_claim);

        return claimRepository.Update(claim);
    }

    public async Task<Claim> UpdateAsync(Claim claim)
    {
        var _claim = await claimRepository.GetAsync(c => c.Id == claim.Id);
        await claimValidations.CheckExistenceAsync(_claim);

        return await claimRepository.UpdateAsync(claim);
    }
}
