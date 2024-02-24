using Core.Repository;
using Entity.Entities;

namespace DataAccess.Abstracts;

public interface IProductRepository : IRepository<Product>, IAsyncRepository<Product>
{
}
