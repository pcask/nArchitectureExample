using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class CardRepository(NADbContext context) : Repository<Card>(context), ICardRepository
{
}
