using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class ProductRepository(DbContext context) : Repository<Product>(context), IProductRepository
{
}
