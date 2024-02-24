using Entity.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class CategoryRepository(NADbContext context) : Repository<Category>(context), ICategoryRepository
{
}
