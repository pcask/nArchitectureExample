using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class ProductTransactionRepository(DbContext context) : Repository<ProductTransaction>(context), IProductTransactionRepository
{
}
