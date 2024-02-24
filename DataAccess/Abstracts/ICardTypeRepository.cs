using Core.Repository;
using Entity.Entities;

namespace DataAccess.Abstracts;

public interface ICardTypeRepository : IRepository<CardType>, IAsyncRepository<CardType>
{
}
