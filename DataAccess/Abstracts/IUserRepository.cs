using Core.Repository;
using Entity.Entities;

namespace DataAccess.Abstracts;

public interface IUserRepository : IRepository<User>, IAsyncRepository<User>
{
}
