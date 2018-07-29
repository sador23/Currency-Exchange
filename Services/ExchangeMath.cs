using CurrencyExchange.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class ExchangeMath
    {
        private readonly Dictionary<string, double> _rates;

        public ExchangeMath(Dictionary<string,double> rates)
        {
            _rates = rates;
        }

        public decimal CalculateExchange(ExchangeInput exchangeInput)
        {
            var rateFrom = _rates.Where(x => x.Key.Equals(exchangeInput.From)).Select(x => x.Value).FirstOrDefault();
            var rateTo = (decimal)_rates.Where(x => x.Key.Equals(exchangeInput.To)).Select(x => x.Value).FirstOrDefault();
            var result = (decimal)(1.0 / rateFrom) * exchangeInput.Value * rateTo;
            return result;
        }
    }
}
