using Entity.Entities;

namespace Entity.ViewModels.ProductTransactions;

public class ProductTransactionVm
{

    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public ProductTransaction GetEntity()
    {
        return new()
        {
            Id = Id,
            ProductId = ProductId,
            Quantity = Quantity
        };
    }

    public static ProductTransactionVm GetModel(ProductTransaction productTransaction)
    {
        return new()
        {
            Id = productTransaction.Id,
            ProductId = productTransaction.ProductId,
            Quantity = productTransaction.Quantity
        };
    }
}
