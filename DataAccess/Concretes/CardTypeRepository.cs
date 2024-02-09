using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class CardTypeRepository(NADbContext context) : Repository<CardType>(context), ICardTypeRepository
{
}
