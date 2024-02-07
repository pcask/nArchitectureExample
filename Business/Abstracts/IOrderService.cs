using Core.DTOs.Order;
using Core.Entities;

namespace Business.Abstracts;

public interface IOrderService
{
    IEnumerable<Order> GetAll();
    Order? GetById(Guid id);

    Order Add(AddOrderDto addOrderDto);
    Order Update(Order order);
    void DeleteById(Guid id);


    Task<IEnumerable<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(Guid id);

    Task<Order> AddAsync(AddOrderDto addOrderDto);
    Task<Order> UpdateAsync(Order order);
    Task DeleteByIdAsync(Guid id);
}
