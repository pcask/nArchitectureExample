using Core.CrossCuttingConcerns.Validation;
using Entity.DTOs.Orders;
using Entity.ViewModels.Orders;

namespace Core.Abstracts;

public interface IOrderService
{
    IEnumerable<OrderListVm> GetAll();
    OrderVm? GetById(Guid id);

    void Add(OrderAddDto addOrderDto);
    void Update(Guid id, OrderUpdateDto orderUpdateDto);
    void DeleteById(Guid id);


    Task<IEnumerable<OrderListVm>> GetAllAsync();
    Task<OrderVm?> GetByIdAsync(Guid id);

    Task AddAsync(OrderAddDto addOrderDto);
    Task UpdateAsync(Guid id, OrderUpdateDto orderUpdateDto);
    Task DeleteByIdAsync(Guid id);
}
