using Core.Entities.Security;

namespace Entity.Entities
{
    public class User : AppUser
    {
        public string IdentificationNumber { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short BirthYear { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = [];
        public virtual ICollection<Card> Cards { get; set; } = [];
    }
}
