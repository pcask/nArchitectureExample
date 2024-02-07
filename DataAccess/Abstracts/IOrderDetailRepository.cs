using Core.Entities;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface IOrderDetailRepository : IRepository<OrderDetail>, IAsyncRepository<OrderDetail>
{
}
