using Entity.Entities;

namespace Entity.DTOs.Orders;

public class AddOrderDto
{
    public Guid UserId { get; set; }
    public IList<ProductTransaction> ProductTransactions { get; set; } = [];
}
