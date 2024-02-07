using Core.Entities.Security;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class ClaimRepository(DbContext context) : Repository<Claim>(context), IClaimRepository
{
}
