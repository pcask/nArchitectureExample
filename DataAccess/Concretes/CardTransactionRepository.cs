using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class CardTransactionRepository(DbContext context) : Repository<CardTransaction>(context), ICardTransactionRepository
{
}
