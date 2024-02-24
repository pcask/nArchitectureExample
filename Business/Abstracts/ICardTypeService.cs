using Entity.Entities;

namespace Business.Abstracts;

public interface ICardTypeService
{
    IEnumerable<CardType> GetAll();
    CardType? GetById(Guid id);

    CardType Add(CardType cardType);
    CardType Update(CardType cardType);
    void DeleteById(Guid id);


    Task<IEnumerable<CardType>> GetAllAsync();
    Task<CardType?> GetByIdAsync(Guid id);

    Task<CardType> AddAsync(CardType cardType);
    Task<CardType> UpdateAsync(CardType cardType);
    Task DeleteByIdAsync(Guid id);
}