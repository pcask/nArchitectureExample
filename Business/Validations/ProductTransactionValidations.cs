using Business.Exceptions;
using Entity.Entities;

namespace Business.Validations;

public class ProductTransactionValidations
{
    public void CheckExistence(ProductTransaction? productTransaction)
    {
        if (productTransaction == null) throw new ValidationException("ProductTransaction not found");
    }

    public async Task CheckExistenceAsync(ProductTransaction? productTransaction)
    {
        await Task.Run(() =>
        {
            if (productTransaction == null)
                throw new ValidationException("ProductTransaction not found");
        });
    }

    public void CheckProductIdList(Guid[] productIdList)
    {
        if (productIdList == null || productIdList.Length < 1)
            throw new ValidationException("ProductIdList cannot be empty!");
    }
    public async Task CheckProductIdListAsync(Guid[] productIdList)
    {
        await Task.Run(() =>
        {
            if (productIdList == null || productIdList.Length < 1)
                throw new ValidationException("ProductIdList cannot be empty!");
        });
    }

    internal void IfAnyProductDoesNotExist(bool doesNotExist)
    {
        if (doesNotExist)
            throw new ValidationException("Some products doesn't exist!");
    }
}
