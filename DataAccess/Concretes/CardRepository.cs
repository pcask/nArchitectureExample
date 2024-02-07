using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class CardRepository(DbContext context) : Repository<Card>(context), ICardRepository
{
}
