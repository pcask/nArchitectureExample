using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;

namespace DataAccess.Concretes;

public class CardTransactionRepository(NADbContext context) : Repository<CardTransaction>(context), ICardTransactionRepository
{
}
