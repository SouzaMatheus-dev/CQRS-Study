using _123Sales.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _123Sales.Infra.Context
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options)
        {
        }

        public DbSet<CustomerExternal> Customers { get; set; }
        public DbSet<ProductExternal> Products { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Sale> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Customer)
                .WithMany(c => c.Sales)
                .HasForeignKey(s => s.CustomerId);

            modelBuilder.Entity<Sale>()
               .Property(s => s.TotalAmount)
               .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleItem>()
                .Property(si => si.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<ProductExternal>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.Items)
                .HasForeignKey(si => si.SaleId);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId);

            modelBuilder.Entity<CustomerExternal>().HasData(
                new CustomerExternal
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "Matheus Neves"
                }
            );

            modelBuilder.Entity<ProductExternal>().HasData(
                new ProductExternal
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Description = "Laptop",
                    UnitPrice = 1500.00m
                }
            );
        }
    }
}