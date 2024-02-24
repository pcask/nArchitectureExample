using Core.Entities.Security;
using Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts;

public class NADbContext : DbContext
{
    //ILoggerFactory loggerFactory = new LoggerFactory();
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseLoggerFactory(loggerFactory)
        //    .EnableSensitiveDataLogging()
        //    .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NAExampleDB;Integrated Security=True");

        optionsBuilder
            .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NAExampleDB;Integrated Security=True")
            .LogTo(Console.WriteLine)
            .EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TPH approach;
        //modelBuilder.Entity<AppUser>()
        //    .HasDiscriminator<bool>("IsUser")
        //    .HasValue<User>(true);

        // TPC approach;
        //modelBuilder.Entity<AppUser>().UseTpcMappingStrategy();

        // TPT approach;
        modelBuilder.Entity<AppUser>()
            .ToTable("AppUsers");
        modelBuilder.Entity<User>()
            .ToTable("Users");

        // Add unique constraint to prevent duplicate data in many-to-many relationship.
        modelBuilder.Entity<UserClaim>()
            .HasIndex(x => new { x.AppUserId, x.ClaimId }).IsUnique();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Claim> Claims { get; set; }
    public DbSet<UserClaim> UserClaims { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductTransaction> ProductTransactions { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<CardType> CardTypes { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardTransaction> CardTransactions { get; set; }
}
