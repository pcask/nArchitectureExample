using Core.Entities.Security;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class ClaimRepository(NADbContext context) : Repository<Claim>(context), IClaimRepository
{
}
