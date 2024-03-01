using Core.Entities.Common;

namespace Entity.Entities
{
    public class Category : Entity<Guid>
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Category()
        {
            Products = [];
        }
    }
}
