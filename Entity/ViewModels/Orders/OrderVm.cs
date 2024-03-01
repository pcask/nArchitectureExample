using Entity.Entities;
using Entity.ViewModels.OrderDetail;
using System.Collections.Generic;

namespace Entity.ViewModels.Orders;

public class OrderVm
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public static OrderVm GetModel(Order order)
    {
        return new()
        {
            Id = order.Id,
            CreatedDate = order.CreatedDate,
            UserId = order.UserId
        };
    }
}