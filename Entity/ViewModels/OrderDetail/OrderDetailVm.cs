using Entity.ViewModels.ProductTransactions;

namespace Entity.ViewModels.OrderDetail;

public class OrderDetailVm
{
    public Guid OrderId { get; set; }
    public string? Status { get; set; }
    public ProductTransactionVm ProductTransaction { get; set; }
}
