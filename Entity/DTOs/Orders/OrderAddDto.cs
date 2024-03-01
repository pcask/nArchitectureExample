using Entity.DTOs.ProductTransactions;
using Entity.ViewModels.ProductTransactions;

namespace Entity.DTOs.Orders;

public class OrderAddDto
{
    public Guid UserId { get; set; }
    public IList<ProductTransactionAddDto> ProductTransactions { get; set; }

    public OrderAddDto()
    {
        ProductTransactions = [];
    }
}
