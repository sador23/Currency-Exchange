using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.DTO
{
    public class Currency
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<DailyRate> Rates { get; set; }
    }
}
