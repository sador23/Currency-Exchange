using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.DTO
{
    public class StatisticHelper
    {
        public string Name { get; set; }

        public Dictionary<DateTime,decimal> rates { get; set; }
    }
}
