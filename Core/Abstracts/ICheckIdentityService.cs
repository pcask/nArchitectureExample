namespace Core.Abstracts;

public interface ICheckIdentityService
{
    bool CheckIdentity(string identificationNumber, string firstName, string lastName, short birthYear);
    Task<bool> CheckIdentityAsync(string identificationNumber, string firstName, string lastName, short birthYear);
}
