namespace Roxosoft.DAL
{
    using Microsoft.EntityFrameworkCore;
    using Roxosoft.Common.Models;

    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<ProductsInOrders> ProductsInOrders { get; set; }
        public DbSet<CartModel> Cart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductsInOrders>()
                .HasKey(t => new { t.ProductUid, t.OrderId });

            modelBuilder.Entity<ProductsInOrders>()
                .HasOne(sc => sc.Order)
                .WithMany(c => c.ProductsInOrders)
                .HasForeignKey(sc => sc.OrderId);
        }
    }
}
