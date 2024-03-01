using Business.Validations.Common;
using Core.Abstracts;
using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions;
using Entity.DTOs.Orders;
using Entity.DTOs.ProductTransactions;

namespace Business.Validations.Orders;

public class OrderAddValidations(IProductTransactionService productTransactionService)
{
    [ValidationMethod(Priority: 0)]
    public async Task CheckProductListAsync(OrderAddDto addOrderDto)
    {
        await Task.Run(() =>
         {
             if (addOrderDto.ProductTransactions.Count == 0)
                 throw new ValidationException("Product list cannot be empty!");
         });
    }

    [ValidationMethod(Priority: 1)]
    public async Task CheckProductQuantityAsync(OrderAddDto addOrderDto)
    {
        await Task.Run(() =>
         {
             if (addOrderDto.ProductTransactions.Any(pt => pt.Quantity < 1))
                 throw new ValidationException("Product quantities must be greater than 0! Please check the product list.");
         });
    }

    [ValidationMethod(Priority: 2)]
    public async Task CheckProductStocksAsync(OrderAddDto orderAddDto)
    {
        var productTransactions = orderAddDto.ProductTransactions.GroupBy(pt => pt.ProductId).Select(group =>
        {
            return new ProductTransactionAddDto()
            {
                ProductId = group.Key,
                Quantity = group.Sum(pt => pt.Quantity)
            };
        });

        var currentStocks = new Dictionary<Guid, int>();

        foreach (var pt in productTransactions)
        {
            currentStocks.Add(pt.ProductId, await productTransactionService.GetStockByProductIdAsync(pt.ProductId));
        }

        if (productTransactions.Any(pt => currentStocks[pt.ProductId] - pt.Quantity < 0))
            throw new ValidationException("There is not enough stock!");
    }
}
