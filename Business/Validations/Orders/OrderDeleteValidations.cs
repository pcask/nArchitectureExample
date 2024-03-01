using DataAccess.Abstracts;

namespace Business.Validations.Orders;

public class OrderDeleteValidations(IOrderRepository orderRepository) : OrderValidations(orderRepository)
{

}
