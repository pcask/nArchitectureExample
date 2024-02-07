using Business.Exceptions;
using Core.Entities;

namespace Business.Validations;

public class CardTypeValidations
{
    public void CheckExistence(CardType? cardType)
    {
        if (cardType == null) throw new ValidationException("CardType not found");
    }

    public async Task CheckExistenceAsync(CardType? cardType)
    {
        await Task.Run(() =>
        {
            if (cardType == null)
                throw new ValidationException("CardType not found");
        });
    }
}
