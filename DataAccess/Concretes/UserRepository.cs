using Core.Entities.Security;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class UserRepository(NADbContext context) : Repository<User>(context), IUserRepository
{
}
