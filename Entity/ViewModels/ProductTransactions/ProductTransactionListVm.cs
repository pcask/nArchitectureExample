using Entity.Entities;

namespace Entity.ViewModels.ProductTransactions;

public class ProductTransactionListVm
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public static IEnumerable<ProductTransactionListVm> GetModels(IEnumerable<ProductTransaction> productTransactions)
    {
        return productTransactions.Select(pt => new ProductTransactionListVm()
        {
            ProductId = pt.ProductId,
            Quantity = pt.Quantity
        });
    }

    public static IEnumerable<ProductTransactionListVm> GetEntities(IEnumerable<ProductTransactionListVm> productTransactionListVm)
    {
        return productTransactionListVm.Select(pt => new ProductTransactionListVm()
        {
            ProductId = pt.ProductId,
            Quantity = pt.Quantity
        });
    }
}
