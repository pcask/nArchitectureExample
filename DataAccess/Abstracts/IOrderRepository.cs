using Core.Entities;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface IOrderRepository : IRepository<Order>, IAsyncRepository<Order>
{
}
