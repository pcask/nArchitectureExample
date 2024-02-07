using Core.Entities.Security;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class UserClaimRepository(DbContext context) : Repository<UserClaim>(context), IUserClaimRepository
{
}
