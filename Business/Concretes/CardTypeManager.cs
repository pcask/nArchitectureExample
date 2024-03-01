using Core.Abstracts;
using Core.Validations;
using Entity.Entities;
using DataAccess.Abstracts;

namespace Core.Concretes;

public class CardTypeManager(ICardTypeRepository cardTypeRepository, CardTypeValidations cardTypeValidations) : ICardTypeService
{
    public CardType Add(CardType cardType)
    {
        return cardTypeRepository.Add(cardType);
    }

    public async Task<CardType> AddAsync(CardType cardType)
    {
        return await cardTypeRepository.AddAsync(cardType);
    }

    public void DeleteById(Guid id)
    {
        var cardType = cardTypeRepository.Get(c => c.Id == id);

        cardTypeValidations.CheckExistence(cardType);
        cardTypeRepository.Delete(cardType);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var cardType = await cardTypeRepository.GetAsync(c => c.Id == id);

        await cardTypeValidations.CheckExistenceAsync(cardType);
        await cardTypeRepository.DeleteAsync(cardType);
    }

    public IEnumerable<CardType> GetAll() => cardTypeRepository.GetAll();

    public async Task<IEnumerable<CardType>> GetAllAsync() => await cardTypeRepository.GetAllAsync();

    public CardType? GetById(Guid id) => cardTypeRepository.Get(c => c.Id == id);

    public async Task<CardType?> GetByIdAsync(Guid id) => await cardTypeRepository.GetAsync(c => c.Id == id);

    public CardType Update(CardType cardType)
    {
        var _cardType = cardTypeRepository.Get(c => c.Id == cardType.Id);
        cardTypeValidations.CheckExistence(_cardType);

        return cardTypeRepository.Update(cardType);
    }

    public async Task<CardType> UpdateAsync(CardType cardType)
    {
        var _cardType = await cardTypeRepository.GetAsync(c => c.Id == cardType.Id);
        await cardTypeValidations.CheckExistenceAsync(_cardType);

        return await cardTypeRepository.UpdateAsync(cardType);
    }
}
