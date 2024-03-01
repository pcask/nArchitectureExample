using Core.Entities.Security;

namespace Entity.DTOs.Claims;

public class ClaimUpdateDto
{
    public string Group { get; set; }
    public string Name { get; set; }

    public Claim GetEntity(Claim beUpdated)
    {
        beUpdated.Group = Group;
        beUpdated.Name = Name;

        return beUpdated;
    }
}