using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace dotnet_pricing_svc.Models
{

    public class PrincingContext : DbContext
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
                .Property<DateTime>("Data")
                .HasColumnType("timestamp without time zone");
            
            modelBuilder.Entity<Price>()
                .HasOne(p => p.Ticker)
                .WithMany(t => t.Prices)
                .HasForeignKey("TickerId");

            modelBuilder.Entity<Ticker>().HasData(
                new Ticker(){ TickerId = 1, Nome = "FLRY3", Tipo = "Acao" },
                new Ticker(){ TickerId = 2, Nome = "ITSA4", Tipo = "Acao" }
            );

            modelBuilder.Entity<Price>().HasData(
                new Price() { PriceId = 1, Data = new DateTime(2022, 6, 10), Valor = 15.16F, TickerId = 1 },
                new Price() { PriceId = 2, Data = new DateTime(2022, 6, 9), Valor = 15.09F, TickerId = 1 },
                new Price() { PriceId = 3, Data = new DateTime(2022, 6, 10), Valor = 8.89F, TickerId = 2 },
                new Price() { PriceId = 4, Data = new DateTime(2022, 6, 9), Valor = 9.07F, TickerId = 2 }
            );
        }
    }

    public class Ticker
    {
        public int TickerId { get; set; }
        public string Nome { get; set; } = String.Empty;
        public string Tipo { get; set; } = String.Empty;

        public ICollection<Price> Prices { get; set; }
    }

    public class Price
    {
        public int PriceId { get; set; }        
        public DateTime Data { get; set; }
        public float Valor { get; set; }
        public int TickerId { get; set; }
        public Ticker Ticker { get; set; }
    }
}