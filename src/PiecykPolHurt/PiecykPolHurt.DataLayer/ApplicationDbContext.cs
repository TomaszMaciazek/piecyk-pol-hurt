using Microsoft.EntityFrameworkCore;
using PiecykPolHurt.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiecykPolHurt.DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrdersLines { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SendPoint> SendPoints { get; set; }
        public DbSet<ProductSendPoint> ProductSendPoints { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<DictionaryType> DictionaryTypes { get; set; }
        public DbSet<DictionaryValue> DictionaryValues { get; set; }
        public DbSet<ReportDefinition> ReportDefinitions { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>()
                .HasMany(x => x.Lines)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SendPoint>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.SendPoint)
                .HasForeignKey(x => x.SendPointId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Order>()
                .HasOne(x => x.Buyer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasMany(x => x.Lines)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasMany(x => x.SendPoints)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<SendPoint>()
                .HasMany(x => x.Products)
                .WithOne(x => x.SendPoint)
                .HasForeignKey(x => x.SendPointId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DictionaryType>()
                .HasMany(x => x.Values)
                .WithOne(x => x.Type)
                .HasForeignKey(x => x.DictionaryTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<DictionaryType>()
                .HasData(
                new DictionaryType { Id = 1, Name = "Order Status" },
                new DictionaryType { Id = 2, Name = "User Role" }
                );

            builder.Entity<DictionaryValue>()
                .HasData(
                    new DictionaryValue { Id = 1, DictionaryTypeId = 1, Value = 0, Description = "Wysłane"},
                    new DictionaryValue { Id = 2, DictionaryTypeId = 1, Value = 1, Description = "Odrzucone" },
                    new DictionaryValue { Id = 3, DictionaryTypeId = 1, Value = 2, Description = "Zaakceptowane" },
                    new DictionaryValue { Id = 4, DictionaryTypeId = 1, Value = 3, Description = "Anulowane" },
                    new DictionaryValue { Id = 5, DictionaryTypeId = 1, Value = 4, Description = "Zakończone" }
                );
        }
    }
}
