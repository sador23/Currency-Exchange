using CurrencyExchange.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Models
{
    public class Composite
    {
        //public int Id { get; set; }
        public int DailyRateId { get; set; }
        public int CurrencyId { get; set; }
        public double Rate { get; set; }
        public Currency Currency { get; set; }
        public DailyRate DailyRate { get; set; }

    }
}
