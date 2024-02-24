using Core.Repository;
using Entity.Entities;

namespace DataAccess.Abstracts;

public interface ICategoryRepository : IRepository<Category>, IAsyncRepository<Category>
{
}
