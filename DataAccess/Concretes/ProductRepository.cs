using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class ProductRepository(NADbContext context) : Repository<Product>(context), IProductRepository
{
}
