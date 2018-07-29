using CurrencyExchange.DTO;
using CurrencyExchange.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Context
{
    public class ExchangerContext : DbContext
    {


        public DbSet<Currency> Currency { get; set; }
        public DbSet<DailyRate> DailyRate { get; set; }

        public ExchangerContext(DbContextOptions<ExchangerContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Composite>()
                .HasKey(x => new { x.CurrencyId , x.DailyRateId });

            modelBuilder.Entity<Composite>()
            .HasOne(x => x.Currency)
            .WithMany(x => x.DailyRates)
            .HasForeignKey(x => x.CurrencyId);

            modelBuilder.Entity<Composite>()
                .HasOne(x => x.DailyRate)
                .WithMany(x => x.Currencies)
                .HasForeignKey(x => x.DailyRateId);
        }

        }
}
