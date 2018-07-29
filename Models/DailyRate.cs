using CurrencyExchange.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CurrencyExchange.DTO
{
    public class DailyRate
    {
        public int DailyRateId { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public ICollection<Composite> Currencies { get; set; }
    }
}
