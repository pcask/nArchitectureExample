using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class OrderRepository(DbContext context) : Repository<Order>(context), IOrderRepository
{
}
