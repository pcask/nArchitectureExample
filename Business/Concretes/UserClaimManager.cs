using Business.Abstracts;
using Business.Validations;
using Core.Entities.Security;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class UserClaimManager(IUserClaimRepository userClaimRepository, UserClaimValidations userClaimValidations) : IUserClaimService
{
    public UserClaim Add(UserClaim userClaim)
    {
        return userClaimRepository.Add(userClaim);
    }

    public async Task<UserClaim> AddAsync(UserClaim userClaim)
    {
        return await userClaimRepository.AddAsync(userClaim);
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

    public UserClaim Update(UserClaim userClaim)
    {
        var _userClaim = userClaimRepository.Get(c => c.Id == userClaim.Id);
        userClaimValidations.CheckExistence(_userClaim);

        return userClaimRepository.Update(userClaim);
    }

    public async Task<UserClaim> UpdateAsync(UserClaim userClaim)
    {
        var _userClaim = await userClaimRepository.GetAsync(c => c.Id == userClaim.Id);
        await userClaimValidations.CheckExistenceAsync(_userClaim);

        return await userClaimRepository.UpdateAsync(userClaim);
    }
}