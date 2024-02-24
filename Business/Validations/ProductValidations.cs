using Business.Exceptions;
using Entity.Entities;

namespace Business.Validations;

public class ProductValidations
{
    public void CheckExistence(Product? product)
    {
        if (product == null) throw new ValidationException("Product not found");
    }

    public async Task CheckExistenceAsync(Product? product)
    {
        await Task.Run(() =>
        {
            if (product == null)
                throw new ValidationException("Product not found");
        });
    }
}
