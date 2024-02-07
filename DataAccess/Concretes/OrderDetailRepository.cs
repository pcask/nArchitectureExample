using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class OrderDetailRepository(DbContext context) : Repository<OrderDetail>(context), IOrderDetailRepository
{
}
