using Core.Entities.Common;

namespace Core.Entities;

public class CardType : Entity<Guid>
{
    public string Name { get; set; } // Mifare, Mifare 4k

    public virtual ICollection<Card> Cards { get; set; }

    public CardType()
    {
        Cards = new HashSet<Card>();
    }
}
