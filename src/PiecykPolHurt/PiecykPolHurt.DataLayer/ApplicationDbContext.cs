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
        public DbSet<OrderLines> OrdersLines { get; set; }
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
        }
    }
}
