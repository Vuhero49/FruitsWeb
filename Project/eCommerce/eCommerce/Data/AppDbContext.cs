using eCommerce.Data.ViewModels;
using eCommerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company_Product>().HasKey(cp => new
            {
                cp.CompanyId,
                cp.ProductId
            });

            modelBuilder.Entity<Company_Product>().HasOne(p => p.Product).WithMany(cp => 
            cp.Company_Products).HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<Company_Product>().HasOne(p => p.Company).WithMany(cp =>
            cp.Company_Products).HasForeignKey(p => p.CompanyId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company_Product> Company_Products { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<City> Cities { get; set; }

        //Orders related tables
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
