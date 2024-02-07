using Core.Entities;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface IProductTransactionRepository : IRepository<ProductTransaction>, IAsyncRepository<ProductTransaction>
{
}
