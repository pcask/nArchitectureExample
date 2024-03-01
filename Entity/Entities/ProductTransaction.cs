using Core.Entities.Common;

namespace Entity.Entities;

public class ProductTransaction : Entity<Guid>
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; }

    public virtual Product Product { get; set; }
}
