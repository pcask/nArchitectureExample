using Core.Abstracts;
using DataAccess.Abstracts;
using Entity.ViewModels.Claims;
using Entity.DTOs.Claims;
using Core.Aspects.Autofac.Validation;
using Business.Validations.Claims;
using Business.Concretes.Common;

namespace Core.Concretes;

public class ClaimManager(IClaimRepository claimRepository) : ManagerBase, IClaimService
{
    public ClaimVm Add(ClaimAddDto claimAddDto)
    {
        var addedClaim = claimRepository.Add(claimAddDto.GetEntity());
        return ClaimVm.GetModel(addedClaim);
    }

    public async Task<ClaimVm> AddAsync(ClaimAddDto claimAddDto)
    {
        var addedClaim = await claimRepository.AddAsync(claimAddDto.GetEntity());
        return ClaimVm.GetModel(addedClaim);
    }

    [ValidationAspect(typeof(ClaimDeleteValidations))]
    public void DeleteById(Guid id)
    {
        claimRepository.Delete(ValidationReturn.Entity);
    }

    [ValidationAspect(typeof(ClaimDeleteValidations))]
    public async Task DeleteByIdAsync(Guid id)
    {
        await claimRepository.DeleteAsync(ValidationReturn.Entity);
    }

    public IEnumerable<ClaimListVm> GetAll()
    {
        return ClaimListVm.GetModels(claimRepository.GetAll());
    }

    public async Task<IEnumerable<ClaimListVm>> GetAllAsync()
    {
        return ClaimListVm.GetModels(await claimRepository.GetAllAsync());
    }

    [ValidationAspect(typeof(ClaimValidations))]
    public ClaimVm? GetById(Guid id)
    {
        return ClaimVm.GetModel(ValidationReturn.Entity);
    }

    [ValidationAspect(typeof(ClaimValidations))]
    public async Task<ClaimVm?> GetByIdAsync(Guid id)
    {
        return await Task.Run(() => ClaimVm.GetModel(ValidationReturn.Entity));
    }

    [ValidationAspect(typeof(ClaimUpdateValidations))]
    public void Update(Guid id, ClaimUpdateDto claimUpdateDto)
    {
        if (ValidationReturn.NoNeedToGoToDb)
            return;

        claimRepository.Update(claimUpdateDto.GetEntity(ValidationReturn.Entity));
    }

    [ValidationAspect(typeof(ClaimUpdateValidations))]
    public async Task UpdateAsync(Guid id, ClaimUpdateDto claimUpdateDto)
    {
        if (ValidationReturn.NoNeedToGoToDb)
            return;

        await claimRepository.Update(claimUpdateDto.GetEntity(ValidationReturn.Entity));
    }
}
