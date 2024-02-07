using Core.Entities;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface ICardRepository : IRepository<Card>, IAsyncRepository<Card>
{
}
