using Business.Abstracts;
using Business.Validations;
using Entity.DTOs.UserClaims;
using DataAccess.Abstracts;
using Entity.ViewModels.UserClaims;

namespace Business.Concretes;

public class UserClaimManager(IUserClaimRepository userClaimRepository, UserClaimValidations userClaimValidations) : IUserClaimService
{
    public void Add(AddUserClaimDto addUserClaimDto)
    {
        userClaimRepository.Add(addUserClaimDto.GetUserClaim());
    }

    public async Task AddAsync(AddUserClaimDto addUserClaimDto)
    {
        await userClaimRepository.AddAsync(addUserClaimDto.GetUserClaim());
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

    public IEnumerable<UserClaimListViewModel> GetAll()
    {
        var userClaims = userClaimRepository.GetAll();

        return UserClaimListViewModel.GetModels(userClaims);
    }

    public async Task<IEnumerable<UserClaimListViewModel>> GetAllAsync()
    {
        var userClaims = await userClaimRepository.GetAllAsync();

        return UserClaimListViewModel.GetModels(userClaims);
    }

    public UserClaimViewModel GetById(Guid id)
    {
        var userClaim = userClaimRepository.Get(c => c.Id == id);
        return UserClaimViewModel.GetModel(userClaim);
    }

    public async Task<UserClaimViewModel> GetByIdAsync(Guid id)
    {
        var userClaim = await userClaimRepository.GetAsync(c => c.Id == id);
        return UserClaimViewModel.GetModel(userClaim);
    }

    public void Update(UpdateUserClaimDto updateUserClaimDto)
    {
        var _userClaim = userClaimRepository.Get(c => c.Id == updateUserClaimDto.Id);
        userClaimValidations.CheckExistence(_userClaim);

        _userClaim.AppUserId = updateUserClaimDto.UserId;
        _userClaim.ClaimId = updateUserClaimDto.ClaimId;

        userClaimRepository.Update(_userClaim);
    }

    public async Task UpdateAsync(UpdateUserClaimDto updateUserClaimDto)
    {
        var _userClaim = await userClaimRepository.GetAsync(c => c.Id == updateUserClaimDto.Id);
        await userClaimValidations.CheckExistenceAsync(_userClaim);

        _userClaim.AppUserId = updateUserClaimDto.UserId;
        _userClaim.ClaimId = updateUserClaimDto.ClaimId;

        await userClaimRepository.UpdateAsync(_userClaim);
    }
}