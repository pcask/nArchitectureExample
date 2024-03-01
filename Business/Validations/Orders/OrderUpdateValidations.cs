using Business.Validations.Common;
using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions;
using DataAccess.Abstracts;
using Entity.DTOs.Orders;
using Microsoft.EntityFrameworkCore;

namespace Business.Validations.Orders;

public class OrderUpdateValidations(IOrderRepository orderRepository) : ValidationBase
{
    [ValidationMethod(Priority: 0)]
    public async Task CheckProductQuantityAsync(OrderAddDto addOrderDto)
    {
        await Task.Run(() =>
        {
            if (addOrderDto.ProductTransactions.Any(pt => pt.Quantity < 1))
                throw new ValidationException("Product quantities must be greater than 0! Please check the product list.");
        });
    }

    [ValidationMethod(Priority: 1)]
    public async Task CheckExistenceAsync(Guid id)
    {
        var order = (await orderRepository
            .GetAsync(o => o.Id == id, include: qO => qO
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.ProductTransaction))) ?? throw new ValidationException("Order not found");

        if (order.OrderDetails.Count < 1)
            throw new ValidationException("There are no products to update");

        ValidationReturn.Entity = order;
    }
}
