using API.Entities;
using API.Entities.OrderAgg;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Contexts : IdentityDbContext<User, Role, int>
    {
        public Contexts(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

       protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);

    builder.Entity<User>()
        .HasOne(a => a.Address)
        .WithOne()
        .HasForeignKey<UserAddress>(a => a.Id)
        .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<Role>()
        .HasData(
            new Role { Id = 1, Name = "Member", NormalizedName = "MEMBER" },
            new Role { Id = 2, Name = "Admin", NormalizedName = "ADMIN" }
        );

    if (Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite")
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(string) && property.GetMaxLength() == null)
                {
                    property.SetColumnType("TEXT");
                }
            }
        }
    }
}

    }
}
