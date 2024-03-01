using Core.Abstracts;
using Core.Validations;
using Entity.DTOs.UserClaims;
using DataAccess.Abstracts;
using Entity.ViewModels.UserClaims;

namespace Core.Concretes;

public class UserClaimManager(IUserClaimRepository userClaimRepository, UserClaimValidations userClaimValidations) : IUserClaimService
{
    public void Add(UserClaimAddDto addUserClaimDto)
    {
        userClaimRepository.Add(addUserClaimDto.GetEntity());
    }

    public async Task AddAsync(UserClaimAddDto addUserClaimDto)
    {
        await userClaimRepository.AddAsync(addUserClaimDto.GetEntity());
    }

    public void DeleteById(Guid id)
    {
        var userClaim = userClaimRepository.Get(c => c.Id == id);

        userClaimValidations.CheckExistence(userClaim);
        userClaimRepository.Delete(userClaim);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var userClaim = await userClaimRepository.GetAsync(c => c.Id == id);

        await userClaimValidations.CheckExistenceAsync(userClaim);
        await userClaimRepository.DeleteAsync(userClaim);
    }

    public IEnumerable<UserClaimListVm> GetAll()
    {
        var userClaims = userClaimRepository.GetAll();

        return UserClaimListVm.GetModels(userClaims);
    }

    public async Task<IEnumerable<UserClaimListVm>> GetAllAsync()
    {
        var userClaims = await userClaimRepository.GetAllAsync();

        return UserClaimListVm.GetModels(userClaims);
    }

    public UserClaimVm GetById(Guid id)
    {
        var userClaim = userClaimRepository.Get(c => c.Id == id);
        return UserClaimVm.GetModel(userClaim);
    }

    public async Task<UserClaimVm> GetByIdAsync(Guid id)
    {
        var userClaim = await userClaimRepository.GetAsync(c => c.Id == id);
        return UserClaimVm.GetModel(userClaim);
    }

    public void Update(UserClaimUpdateDto updateUserClaimDto)
    {
        var _userClaim = userClaimRepository.Get(c => c.Id == updateUserClaimDto.Id);
        userClaimValidations.CheckExistence(_userClaim);

        _userClaim.AppUserId = updateUserClaimDto.UserId;
        _userClaim.ClaimId = updateUserClaimDto.ClaimId;

        userClaimRepository.Update(_userClaim);
    }

    public async Task UpdateAsync(UserClaimUpdateDto updateUserClaimDto)
    {
        var _userClaim = await userClaimRepository.GetAsync(c => c.Id == updateUserClaimDto.Id);
        await userClaimValidations.CheckExistenceAsync(_userClaim);

        _userClaim.AppUserId = updateUserClaimDto.UserId;
        _userClaim.ClaimId = updateUserClaimDto.ClaimId;

        await userClaimRepository.UpdateAsync(_userClaim);
    }
}