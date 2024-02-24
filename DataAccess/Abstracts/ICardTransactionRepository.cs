using Core.Repository;
using Entity.Entities;

namespace DataAccess.Abstracts;

public interface ICardTransactionRepository : IRepository<CardTransaction>, IAsyncRepository<CardTransaction>
{
}
