﻿using Core.Repository.EFCore;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entity.Entities;

namespace DataAccess.Concretes;

public class CardRepository(NADbContext context) : Repository<Card>(context), ICardRepository
{
   
}
