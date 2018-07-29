using CurrencyExchange.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.DTO
{
    public class Currency
    {
        public int CurrencyId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Composite> DailyRates { get; set; }
    }
}
