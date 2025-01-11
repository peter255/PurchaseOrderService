using Microsoft.EntityFrameworkCore;
using PurchaseOrderService.Domain.Entities;

namespace PurchaseOrderService.Infrastructure.Persistence
{
    public class PurchaseOrderDbContext : DbContext
    {
        public PurchaseOrderDbContext(DbContextOptions<PurchaseOrderDbContext> options) : base(options) { }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure PurchaseOrder
            modelBuilder.Entity<PurchaseOrder>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.PONumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.PODate).IsRequired();

                // Configure Money as an Owned Type for TotalPrice
                entity.OwnsOne(e => e.TotalPrice, builder =>
                {
                    builder.Property(m => m.Amount)
                           .IsRequired()
                           .HasPrecision(18, 2)
                           .HasColumnName("TotalPrice_Amount");

                    builder.Property(m => m.Currency)
                           .IsRequired()
                           .HasMaxLength(5)
                           .HasColumnName("TotalPrice_Currency");
                });
            });

            // Configure PurchaseOrderItem
            modelBuilder.Entity<PurchaseOrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.GoodCode).IsRequired().HasMaxLength(20);

                // Configure Money as an Owned Type for Price
                entity.OwnsOne(e => e.Price, builder =>
                {
                    builder.Property(m => m.Amount)
                           .IsRequired()
                           .HasPrecision(18, 2)
                           .HasColumnName("Price_Amount");

                    builder.Property(m => m.Currency)
                           .IsRequired()
                           .HasColumnName("Price_Currency");
                });
            });
        }

    }
}
