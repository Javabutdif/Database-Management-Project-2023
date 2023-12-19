using Microsoft.EntityFrameworkCore;
using Website_Database_New.Models;

namespace Website_Database_New.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<CustomerData> Customers { get; set; }
        public DbSet<ItemData> Items { get; set; }
        public DbSet<CartData> Cart { get; set; }
        public DbSet<Cart_Item> Cart_Item { get; set; }
        public DbSet<OrderDetailsData> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartData>() // Replace with your actual entity
                .HasKey(e => e.cart_id);

            modelBuilder.Entity<Cart_Item>()
                 .HasKey(e => e.cart_item_id);
            modelBuilder.Entity<Cart_Item>()
              .HasKey(e => e.item_id);
            modelBuilder.Entity<OrderData>()
                .HasKey(e => e.c_id);
        }
        public DbSet<OrderData> Orders { get; set; }

    }
}

