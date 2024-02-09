using Business.Abstracts;
using Business.Validations;
using Core.DTOs.UserClaim;
using Core.Entities.Security;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class UserClaimManager(IUserClaimRepository userClaimRepository, UserClaimValidations userClaimValidations) : IUserClaimService
{
    public UserClaim Add(AddUserClaimDto userClaimDto)
    {
        return userClaimRepository.Add(new() { UserId = userClaimDto.UserId, ClaimId = userClaimDto.ClaimId });
    }

    public async Task<UserClaim> AddAsync(AddUserClaimDto userClaimDto)
    {
        return await userClaimRepository.AddAsync(new() { UserId = userClaimDto.UserId, ClaimId = userClaimDto.ClaimId });
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

    public IEnumerable<UserClaim> GetAll() => userClaimRepository.GetAll();

    public async Task<IEnumerable<UserClaim>> GetAllAsync() => await userClaimRepository.GetAllAsync();

    public UserClaim? GetById(Guid id) => userClaimRepository.Get(c => c.Id == id);

    public async Task<UserClaim?> GetByIdAsync(Guid id) => await userClaimRepository.GetAsync(c => c.Id == id);

    public UserClaim Update(UpdateUserClaimDto userClaimDto)
    {
        var _userClaim = userClaimRepository.Get(c => c.Id == userClaimDto.Id);
        userClaimValidations.CheckExistence(_userClaim);

        _userClaim.UserId = userClaimDto.UserId;
        _userClaim.ClaimId = userClaimDto.ClaimId;

        return userClaimRepository.Update(_userClaim);
    }

    public async Task<UserClaim> UpdateAsync(UpdateUserClaimDto userClaimDto)
    {
        var _userClaim = await userClaimRepository.GetAsync(c => c.Id == userClaimDto.Id);
        await userClaimValidations.CheckExistenceAsync(_userClaim);

        _userClaim.UserId = userClaimDto.UserId;
        _userClaim.ClaimId = userClaimDto.ClaimId;

        return await userClaimRepository.UpdateAsync(_userClaim);
    }
}