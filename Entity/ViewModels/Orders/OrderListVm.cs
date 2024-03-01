using Entity.Entities;

namespace Entity.ViewModels.Orders;

public class OrderListVm
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public static IEnumerable<OrderListVm> GetModels(IEnumerable<Order> orders)
    {
        return orders.Select(o => new OrderListVm
        {
            Id = o.Id,
            UserId = o.UserId,
            CreatedDate = o.CreatedDate
        });
    }
}
