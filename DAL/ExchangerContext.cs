using CurrencyExchange.DTO;
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

    }
}
