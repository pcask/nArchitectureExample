using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entity.Entities;

namespace DataAccess.Concretes;

public class ProductRepository(NADbContext context) : Repository<Product>(context), IProductRepository
{
}
