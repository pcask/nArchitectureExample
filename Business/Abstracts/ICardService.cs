using Core.Entities;

namespace Business.Abstracts;

public interface ICardService
{
    IEnumerable<Card> GetAll();
    Card? GetById(Guid id);

    Card Add(Card card);
    Card Update(Card card);
    void DeleteById(Guid id);


    Task<IEnumerable<Card>> GetAllAsync();
    Task<Card?> GetByIdAsync(Guid id);

    Task<Card> AddAsync(Card card);
    Task<Card> UpdateAsync(Card card);
    Task DeleteByIdAsync(Guid id);
}
