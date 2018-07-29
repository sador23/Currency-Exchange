using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.DTO
{
    public class ExchangeInput
    {
        public string From { get; set; }
        public string To { get; set; }
        [DataType(DataType.Currency)]
        public decimal Value { get; set; }
    }
}
