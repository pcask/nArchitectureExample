using Core.Entities.Common;

namespace Entity.Entities;

public class Card : Entity<Guid>
{
    public Guid UserId { get; set; }
    public Guid CardTypeId { get; set; }
    public long CardUID { get; set; } // Mifare card içerisindeki Card Unique ID
    public decimal Limit { get; set; } // Düşülebilecek eksi bakiye sınırı

    public virtual User User { get; set; }
    public virtual CardType CardType { get; set; }
    public virtual ICollection<CardTransaction> CardTransactions { get; set; } = [];
}
