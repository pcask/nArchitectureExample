using NufusMudurlugu;

namespace Infrastructure.Services;

public static class CheckIdentityByTurkey
{
    private static KPSPublicSoapClient kpsClient = new(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap12);

    public static async Task<bool> CheckIdentityAsync(string identificationNumber, string firstName, string lastName, short birthYear)
    {
        var result = await kpsClient.TCKimlikNoDogrulaAsync(CastIdToLong(identificationNumber), firstName, lastName, birthYear);
        return result.Body.TCKimlikNoDogrulaResult;
    }

    public static bool CheckIdentity(string identificationNumber, string firstName, string lastName, short birthYear)
    {
        var task = kpsClient.TCKimlikNoDogrulaAsync(CastIdToLong(identificationNumber), firstName, lastName, birthYear);
        return task.GetAwaiter().GetResult().Body.TCKimlikNoDogrulaResult;
    }

    public static long CastIdToLong(string identificationNumber)
    {
        if (!long.TryParse(identificationNumber, out long id))
            id = 0;

        return id;
    }
}
