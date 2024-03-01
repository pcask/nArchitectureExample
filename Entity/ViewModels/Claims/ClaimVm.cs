using Core.Entities.Security;

namespace Entity.ViewModels.Claims;

public class ClaimVm
{
    public Guid Id { get; set; }
    public string Group { get; set; }
    public string Name { get; set; }

    public Claim GetEntity()
    {
        return new()
        {
            Id = Id,
            Group = Group,
            Name = Name
        };
    }

    public static ClaimVm GetModel(Claim claim)
    {
        return new()
        {
            Id = claim.Id,
            Group = claim.Group,
            Name = claim.Name
        };
    }
}
