using Business.Abstracts;
using Business.Validations;
using Core.Entities;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class CardManager(ICardRepository cardRepository, CardValidations cardValidations) : ICardService
{
    public Card Add(Card card)
    {
        return cardRepository.Add(card);
    }

    public async Task<Card> AddAsync(Card card)
    {
        return await cardRepository.AddAsync(card);
    }

    public void DeleteById(Guid id)
    {
        var card = cardRepository.Get(c => c.Id == id);

        cardValidations.CheckExistence(card);
        cardRepository.Delete(card);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var card = await cardRepository.GetAsync(c => c.Id == id);

        await cardValidations.CheckExistenceAsync(card);
        await cardRepository.DeleteAsync(card);
    }

    public IEnumerable<Card> GetAll() => cardRepository.GetAll();

    public async Task<IEnumerable<Card>> GetAllAsync() => await cardRepository.GetAllAsync();

    public Card? GetById(Guid id) => cardRepository.Get(c => c.Id == id);

    public async Task<Card?> GetByIdAsync(Guid id) => await cardRepository.GetAsync(c => c.Id == id);

    public Card Update(Card card)
    {
        var _card = cardRepository.Get(c => c.Id == card.Id);
        cardValidations.CheckExistence(_card);

        return cardRepository.Update(card);
    }

    public async Task<Card> UpdateAsync(Card card)
    {
        var _card = await cardRepository.GetAsync(c => c.Id == card.Id);
        await cardValidations.CheckExistenceAsync(_card);

        return await cardRepository.UpdateAsync(card);
    }
}
