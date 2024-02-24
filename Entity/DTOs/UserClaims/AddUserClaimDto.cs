﻿using Core.Entities.Security;

namespace Entity.DTOs.UserClaims;

public class AddUserClaimDto
{
    public Guid UserId { get; set; }
    public Guid ClaimId { get; set; }

    public UserClaim GetUserClaim()
    {
        return new UserClaim()
        {
            AppUserId = UserId,
            ClaimId = ClaimId
        };
    }
}
