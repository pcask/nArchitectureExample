using Business.Exceptions;
using Core.Entities;

namespace Business.Validations;

public class CardTransactionValidations
{
    public void CheckExistence(CardTransaction? cardTransaction)
    {
        if (cardTransaction == null) throw new ValidationException("CardTransaction not found");
    }

    public async Task CheckExistenceAsync(CardTransaction? cardTransaction)
    {
        await Task.Run(() =>
        {
            if (cardTransaction == null)
                throw new ValidationException("CardTransaction not found");
        });
    }
}
