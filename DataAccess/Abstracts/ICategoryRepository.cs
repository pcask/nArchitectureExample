using Core.Entities;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface ICategoryRepository : IRepository<Category>, IAsyncRepository<Category>
{
}
