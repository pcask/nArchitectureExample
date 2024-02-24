using Entity.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class OrderRepository(NADbContext context) : Repository<Order>(context), IOrderRepository
{
}
