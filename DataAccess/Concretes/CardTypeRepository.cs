using Core.Entities;
using Core.Repository.EFCore;
using DataAccess.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concretes;

public class CardTypeRepository(DbContext context) : Repository<CardType>(context), ICardTypeRepository
{
}
