using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dotnet_pricing_svc.Models
{

    public class PricingContext : DbContext
    {
        public DbSet<Ticker>? Tickers { get; set; }
        public DbSet<Price>? Prices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=docker;Host=localhost;Port=5432;Database=pricing;Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Price>()
                .Property<DateTime>("Date")
                .HasColumnType("timestamp without time zone");

            modelBuilder.Entity<Price>()
                .HasIndex(p => new { p.TickerId, p.Date }, "IX_Price_UQ")
                .IsUnique();
            
            modelBuilder.Entity<Price>()
                .HasOne(p => p.Ticker)
                .WithMany(t => t.Prices)
                .HasForeignKey("TickerId");

            modelBuilder.Entity<Ticker>()
                .HasIndex(t => t.Name, "IX_Ticker_UQ")
                .IsUnique();

            modelBuilder.Entity<Ticker>().HasData(
                new Ticker(){ TickerId = 1, Name = "FLRY3", Type = "Acao" },
                new Ticker(){ TickerId = 2, Name = "ITSA4", Type = "Acao" }
            );

            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 1, Date = new DateTime(2022, 6, 10), Value = 15.16F, TickerId = 1 },
                new Price() { PriceId = 2, Date = new DateTime(2022, 6, 9), Value = 15.09F, TickerId = 1 },
                new Price() { PriceId = 3, Date = new DateTime(2022, 6, 10), Value = 8.89F, TickerId = 2 },
                new Price() { PriceId = 4, Date = new DateTime(2022, 6, 9), Value = 9.07F, TickerId = 2 }
            );
        }
    }
}