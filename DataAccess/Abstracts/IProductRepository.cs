using Core.Entities;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface IProductRepository : IRepository<Product>, IAsyncRepository<Product>
{
}
