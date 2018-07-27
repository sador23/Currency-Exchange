using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.DTO
{
    public class DailyRate
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double Rate { get; set; }

        public Currency Currency { get; set; }

        public ICollection<Currency> Currencies { get; set; }
    }
}
