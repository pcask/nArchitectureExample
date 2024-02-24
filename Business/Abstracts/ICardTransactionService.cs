using Entity.Entities;

namespace Business.Abstracts;

public interface ICardTransactionService
{
    IEnumerable<CardTransaction> GetAll();
    CardTransaction? GetById(Guid id);

    CardTransaction Add(CardTransaction cardTransaction);
    CardTransaction Update(CardTransaction cardTransaction);
    void DeleteById(Guid id);


    Task<IEnumerable<CardTransaction>> GetAllAsync();
    Task<CardTransaction?> GetByIdAsync(Guid id);

    Task<CardTransaction> AddAsync(CardTransaction cardTransaction);
    Task<CardTransaction> UpdateAsync(CardTransaction cardTransaction);
    Task DeleteByIdAsync(Guid id);
}
