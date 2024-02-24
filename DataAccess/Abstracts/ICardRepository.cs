using Core.Repository;
using Entity.Entities;

namespace DataAccess.Abstracts;

public interface ICardRepository : IRepository<Card>, IAsyncRepository<Card>
{
}
