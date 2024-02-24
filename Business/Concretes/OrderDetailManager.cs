using Business.Abstracts;
using Business.Validations;
using Entity.Entities;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class OrderDetailManager(IOrderDetailRepository orderDetailRepository, OrderDetailValidations orderDetailValidations) : IOrderDetailService
{
    public OrderDetail Add(OrderDetail orderDetail)
    {
        return orderDetailRepository.Add(orderDetail);
    }

    public async Task<OrderDetail> AddAsync(OrderDetail orderDetail)
    {
        return await orderDetailRepository.AddAsync(orderDetail);
    }

    public void DeleteById(Guid id)
    {
        var orderDetail = orderDetailRepository.Get(c => c.Id == id);

        orderDetailValidations.CheckExistence(orderDetail);
        orderDetailRepository.Delete(orderDetail);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var orderDetail = await orderDetailRepository.GetAsync(c => c.Id == id);

        await orderDetailValidations.CheckExistenceAsync(orderDetail);
        await orderDetailRepository.DeleteAsync(orderDetail);
    }

    public IEnumerable<OrderDetail> GetAll() => orderDetailRepository.GetAll();

    public async Task<IEnumerable<OrderDetail>> GetAllAsync() => await orderDetailRepository.GetAllAsync();

    public OrderDetail? GetById(Guid id) => orderDetailRepository.Get(c => c.Id == id);

    public async Task<OrderDetail?> GetByIdAsync(Guid id) => await orderDetailRepository.GetAsync(c => c.Id == id);

    public OrderDetail Update(OrderDetail orderDetail)
    {
        var _orderDetail = orderDetailRepository.Get(c => c.Id == orderDetail.Id);
        orderDetailValidations.CheckExistence(_orderDetail);

        return orderDetailRepository.Update(orderDetail);
    }

    public async Task<OrderDetail> UpdateAsync(OrderDetail orderDetail)
    {
        var _orderDetail = await orderDetailRepository.GetAsync(c => c.Id == orderDetail.Id);
        await orderDetailValidations.CheckExistenceAsync(_orderDetail);

        return await orderDetailRepository.UpdateAsync(orderDetail);
    }
}