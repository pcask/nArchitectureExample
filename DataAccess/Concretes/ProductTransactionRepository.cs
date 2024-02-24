using Entity.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class ProductTransactionRepository(NADbContext context) : Repository<ProductTransaction>(context), IProductTransactionRepository
{
}
