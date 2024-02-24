using Business.Exceptions;
using Entity.Entities;

namespace Business.Validations;

public class OrderValidations
{
    public void CheckExistence(Order? order)
    {
        if (order == null) throw new ValidationException("Order not found");
    }

    public async Task CheckExistenceAsync(Order? order)
    {
        await Task.Run(() =>
        {
            if (order == null)
                throw new ValidationException("Order not found");
        });
    }

    public void CheckProductList(IList<ProductTransaction> productTransactions)
    {
        if (productTransactions.Count() == 0)
            throw new ValidationException("Product list cannot be empty!");
    }

    public void CheckProductQuantity(IList<ProductTransaction> productTransactions)
    {
        if (productTransactions.Any(t => t.Quantity < 1))
            throw new ValidationException("Product quantity must be greater than 0! Please check the product list.");
    }

    public void CheckProductStocks(IList<ProductTransaction> productTransactions, IDictionary<Guid, int> productStocks)
    {
        var firstState = productStocks.Values.Any(q => q < 1);

        var afterDeducting = productTransactions.Select(pt => productStocks[pt.Id] - pt.Quantity).Any(q => q < 0);

        if (firstState || afterDeducting)
            throw new ValidationException("There is not enough stock!");
    }
}
