using Core.Entities;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface ICardTypeRepository : IRepository<CardType>, IAsyncRepository<CardType>
{
}
