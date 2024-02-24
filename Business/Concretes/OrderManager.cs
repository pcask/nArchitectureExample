using Business.Abstracts;
using Business.Validations;
using Entity.DTOs.Orders;
using Entity.Entities;
using DataAccess.Abstracts;
using Core.Aspects.Autofac.Transaction;

namespace Business.Concretes;

public class OrderManager(IOrderRepository orderRepository,
                          OrderValidations orderValidations,
                          IProductTransactionService productTransactionService,
                          IOrderDetailService orderDetailService)
    : IOrderService
{

    [TransactionScopeAspect]
    public Order Add(AddOrderDto addOrderDto)
    {
        orderValidations.CheckProductList(addOrderDto.ProductTransactions);
        orderValidations.CheckProductQuantity(addOrderDto.ProductTransactions);

        var productStocks = productTransactionService.GetStockByProductIdList(addOrderDto.ProductTransactions.Select(pt => pt.Id).ToArray());
        orderValidations.CheckProductStocks(addOrderDto.ProductTransactions, productStocks);

        var order = orderRepository.Add(new()
        {
            UserId = addOrderDto.UserId,
            CreatedDate = DateTime.UtcNow
        });

        addOrderDto.ProductTransactions.ToList().ForEach(pt =>
        {
            pt.Quantity = pt.Quantity > 0 ? -1 * pt.Quantity : pt.Quantity;
            var addedProductTransaction = productTransactionService.Add(pt);

            orderDetailService.Add(new()
            {
                OrderId = order.Id,
                ProductTransactionId = addedProductTransaction.Id,
                Status = "Preparing"
            });
        });

        return order;
    }

    [TransactionScopeAspect]
    public async Task<Order> AddAsync(AddOrderDto addOrderDto)
    {
        return await Task.Run(() =>
         {
             orderValidations.CheckProductList(addOrderDto.ProductTransactions);
             orderValidations.CheckProductQuantity(addOrderDto.ProductTransactions);

             var productStocks = productTransactionService.GetStockByProductIdList(addOrderDto.ProductTransactions.Select(pt => pt.Id).ToArray());
             orderValidations.CheckProductStocks(addOrderDto.ProductTransactions, productStocks);

             var order = orderRepository.Add(new()
             {
                 UserId = addOrderDto.UserId,
                 CreatedDate = DateTime.UtcNow
             });

             addOrderDto.ProductTransactions.ToList().ForEach(pt =>
             {
                 pt.Quantity = pt.Quantity > 0 ? -1 * pt.Quantity : pt.Quantity;
                 var addedProductTransaction = productTransactionService.Add(pt);

                 orderDetailService.Add(new()
                 {
                     OrderId = order.Id,
                     ProductTransactionId = addedProductTransaction.Id,
                     Status = "Preparing"
                 });
             });

             return order;
         });
    }

    public void DeleteById(Guid id)
    {
        var order = orderRepository.Get(c => c.Id == id);

        orderValidations.CheckExistence(order);
        orderRepository.Delete(order);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var order = await orderRepository.GetAsync(c => c.Id == id);

        await orderValidations.CheckExistenceAsync(order);
        await orderRepository.DeleteAsync(order);
    }

    public IEnumerable<Order> GetAll() => orderRepository.GetAll();

    public async Task<IEnumerable<Order>> GetAllAsync() => await orderRepository.GetAllAsync();

    public Order? GetById(Guid id) => orderRepository.Get(c => c.Id == id);

    public async Task<Order?> GetByIdAsync(Guid id) => await orderRepository.GetAsync(c => c.Id == id);

    public Order Update(Order order)
    {
        var _order = orderRepository.Get(c => c.Id == order.Id);
        orderValidations.CheckExistence(_order);

        return orderRepository.Update(order);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        var _order = await orderRepository.GetAsync(c => c.Id == order.Id);
        await orderValidations.CheckExistenceAsync(_order);

        return await orderRepository.UpdateAsync(order);
    }
}