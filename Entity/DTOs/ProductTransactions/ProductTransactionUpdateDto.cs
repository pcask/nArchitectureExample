using Entity.Entities;

namespace Entity.DTOs.ProductTransactions;

public class ProductTransactionUpdateDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }

    public static ProductTransactionUpdateDto GetModel(ProductTransaction pt)
    {
        return new()
        {
            ProductId = pt.ProductId,
            Quantity = pt.Quantity
        };
    }
}