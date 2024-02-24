using Entity.Entities;

namespace Business.Abstracts;

public interface IOrderDetailService
{
    IEnumerable<OrderDetail> GetAll();
    OrderDetail? GetById(Guid id);

    OrderDetail Add(OrderDetail orderDetail);
    OrderDetail Update(OrderDetail orderDetail);
    void DeleteById(Guid id);


    Task<IEnumerable<OrderDetail>> GetAllAsync();
    Task<OrderDetail?> GetByIdAsync(Guid id);

    Task<OrderDetail> AddAsync(OrderDetail orderDetail);
    Task<OrderDetail> UpdateAsync(OrderDetail orderDetail);
    Task DeleteByIdAsync(Guid id);
}
