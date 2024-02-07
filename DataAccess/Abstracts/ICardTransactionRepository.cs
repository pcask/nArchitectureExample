using Core.Entities;
using Core.Repository;

namespace DataAccess.Abstracts;

public interface ICardTransactionRepository : IRepository<CardTransaction>, IAsyncRepository<CardTransaction>
{
}
