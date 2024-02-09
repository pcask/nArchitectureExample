using Core.Abstracts;

namespace Infrastructure.Adapters;

public class CheckIdentityUSAAdapter : ICheckIdentityService
{
    public bool CheckIdentity(string identificationNumber, string firstName, string lastName, short birthYear)
    {
        // Amerika nüfüs müdürlüğü kimlik doğrulama servisi
        return false;
    }

    public async Task<bool> CheckIdentityAsync(string identificationNumber, string firstName, string lastName, short birthYear)
    {
        // Amerika nüfüs müdürlüğü kimlik doğrulama servisi
        await Task.CompletedTask;
        return false;
    }
}
