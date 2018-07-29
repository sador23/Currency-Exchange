using CurrencyExchange.DTO;

namespace CurrencyExchange.Models
{
    public class Composite
    {
        public int DailyRateId { get; set; }
        public int CurrencyId { get; set; }
        public double Rate { get; set; }
        public Currency Currency { get; set; }
        public DailyRate DailyRate { get; set; }

    }
}
