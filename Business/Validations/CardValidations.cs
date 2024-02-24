using Business.Exceptions;
using Entity.Entities;

namespace Business.Validations;

public class CardValidations
{
    public void CheckExistence(Card? card)
    {
        if (card == null) throw new ValidationException("Card not found");
    }

    public async Task CheckExistenceAsync(Card? card)
    {
        await Task.Run(() =>
        {
            if (card == null)
                throw new ValidationException("Card not found");
        });
    }
}
