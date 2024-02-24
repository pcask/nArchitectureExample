using Core.Entities.Common;

namespace Entity.Entities;

public class CardTransaction : Entity<Guid>
{
    public Guid CardId { get; set; }
    public DateTime CreatedDate { get; set; }
    public decimal Balance { get; set; }

    public virtual Card Card { get; set; }
}