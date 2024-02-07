using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class CategoryRepository(DbContext context) : Repository<Category>(context), ICategoryRepository
{
}
