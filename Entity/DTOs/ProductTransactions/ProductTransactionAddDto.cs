using Entity.Entities;

namespace Entity.DTOs.ProductTransactions;

public class ProductTransactionAddDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; }


    public ProductTransaction GetEntity()
    {
        return new()
        {
            ProductId = ProductId,
            Quantity = Quantity,
            CreatedDate = CreatedDate,
        };
    }
}
