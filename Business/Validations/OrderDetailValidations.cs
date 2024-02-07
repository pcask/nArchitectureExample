using Business.Exceptions;
using Core.Entities;

namespace Business.Validations;

public class OrderDetailValidations
{
    public void CheckExistence(OrderDetail? orderDetail)
    {
        if (orderDetail == null) throw new ValidationException("OrderDetail not found");
    }

    public async Task CheckExistenceAsync(OrderDetail? orderDetail)
    {
        await Task.Run(() =>
        {
            if (orderDetail == null)
                throw new ValidationException("OrderDetail not found");
        });
    }
}
