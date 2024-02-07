using Core.Entities.Common;

namespace Core.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<ProductTransaction> ProductTransactions { get; set; }

    public Product()
    {
        ProductTransactions = new HashSet<ProductTransaction>();
    }
}
