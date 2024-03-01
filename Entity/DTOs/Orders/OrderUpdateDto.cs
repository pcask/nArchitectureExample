using Entity.DTOs.ProductTransactions;

namespace Entity.DTOs.Orders;

public class OrderUpdateDto
{
    public Guid UserId { get; set; }
    public List<ProductTransactionUpdateDto> ProductTransactions { get; set; }

    public OrderUpdateDto()
    {
        ProductTransactions = [];
    }
}