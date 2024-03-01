using Core.Abstracts;
using Core.Validations;
using Entity.Entities;
using DataAccess.Abstracts;

namespace Core.Concretes;

public class CardTransactionManager(
                                    ICardTransactionRepository cardTransactionRepository,
                                    CardTransactionValidations cardTransactionValidations)
    : ICardTransactionService
{
    public CardTransaction Add(CardTransaction cardTransaction)
    {
        return cardTransactionRepository.Add(cardTransaction);
    }

    public async Task<CardTransaction> AddAsync(CardTransaction cardTransaction)
    {
        return await cardTransactionRepository.AddAsync(cardTransaction);
    }

    public void DeleteById(Guid id)
    {
        var cardTransaction = cardTransactionRepository.Get(c => c.Id == id);

        cardTransactionValidations.CheckExistence(cardTransaction);
        cardTransactionRepository.Delete(cardTransaction);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var cardTransaction = await cardTransactionRepository.GetAsync(c => c.Id == id);

        await cardTransactionValidations.CheckExistenceAsync(cardTransaction);
        await cardTransactionRepository.DeleteAsync(cardTransaction);
    }

    public IEnumerable<CardTransaction> GetAll() => cardTransactionRepository.GetAll();

    public async Task<IEnumerable<CardTransaction>> GetAllAsync() => await cardTransactionRepository.GetAllAsync();

    public CardTransaction? GetById(Guid id) => cardTransactionRepository.Get(c => c.Id == id);

    public async Task<CardTransaction?> GetByIdAsync(Guid id) => await cardTransactionRepository.GetAsync(c => c.Id == id);

    public CardTransaction Update(CardTransaction cardTransaction)
    {
        var _cardTransaction = cardTransactionRepository.Get(c => c.Id == cardTransaction.Id);
        cardTransactionValidations.CheckExistence(_cardTransaction);

        return cardTransactionRepository.Update(cardTransaction);
    }

    public async Task<CardTransaction> UpdateAsync(CardTransaction cardTransaction)
    {
        var _cardTransaction = await cardTransactionRepository.GetAsync(c => c.Id == cardTransaction.Id);
        await cardTransactionValidations.CheckExistenceAsync(_cardTransaction);

        return await cardTransactionRepository.UpdateAsync(cardTransaction);
    }
}

