using Core.Entities.Security;

namespace Entity.DTOs.Claims;

public class ClaimAddDto
{
    public string Group { get; set; }
    public string Name { get; set; }

    public Claim GetEntity()
    {
        return new()
        {
            Group = Group,
            Name = Name
        };
    }
}
