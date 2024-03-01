using Core.Entities.Security;

namespace Entity.ViewModels.Claims;

public class ClaimListVm
{
    public Guid Id { get; set; }
    public string Group { get; set; }
    public string Name { get; set; }

    public static IEnumerable<ClaimListVm> GetModels(IEnumerable<Claim> claims)
    {
        return claims.Select(c => new ClaimListVm
        {
            Id = c.Id,
            Group = c.Group,
            Name = c.Name
        });
    }
}
