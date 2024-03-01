using Business.Validations.Common;
using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions;
using DataAccess.Abstracts;

namespace Business.Validations.Orders;

public class OrderValidations(IOrderRepository orderRepository): ValidationBase
{
    [ValidationMethod(Priority: 0)]
    public async Task CheckExistenceAsync(Guid id)
    {
        ValidationReturn.Entity = await orderRepository.GetAsync(o => o.Id == id) ?? throw new ValidationException("Order not found");
    }
}