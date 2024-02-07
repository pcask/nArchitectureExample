using Core.Entities.Security;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface IUserRepository : IRepository<User>, IAsyncRepository<User>
{
}
