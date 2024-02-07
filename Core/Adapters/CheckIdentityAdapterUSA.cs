using Core.Abstracts;

namespace Core.Adapters;

public class CheckIdentityAdapterUSA : ICheckIdentityService
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
