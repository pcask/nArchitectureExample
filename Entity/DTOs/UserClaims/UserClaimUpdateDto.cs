namespace Entity.DTOs.UserClaims;

public class UserClaimUpdateDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid ClaimId { get; set; }
}
