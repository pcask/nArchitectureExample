using Core.Abstracts;
using Infrastructure.Services;

namespace Core.Adapters;

public class CheckIdentityAdapterTR : ICheckIdentityService
{
    public bool CheckIdentity(string identificationNumber, string firstName, string lastName, short birthYear)
    {
        return CheckIdentityByTurkey.CheckIdentity(identificationNumber, firstName, lastName, birthYear);
    }

    public async Task<bool> CheckIdentityAsync(string identificationNumber, string firstName, string lastName, short birthYear)
    {
        return await CheckIdentityByTurkey.CheckIdentityAsync(identificationNumber, firstName, lastName, birthYear);
    }
}
