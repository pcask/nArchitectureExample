using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class OrderDetailRepository(NADbContext context) : Repository<OrderDetail>(context), IOrderDetailRepository
{
}
