using Microsoft.EntityFrameworkCore;
using shop.entity;

namespace shop.data.Concrete.EfCore
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> ?Products { get; set; }
        public DbSet<Category> ?Categories { get; set; }
        public DbSet<Cart> ?Carts { get; set; }
        public DbSet<CartItem> ?CartItems { get; set; }
        public DbSet<Order>? Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(local); Initial catalog=dataYedek2; trusted_connection=yes");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                .HasKey(c => new { c.CategoryId, c.ProductId });
        }


    }
}