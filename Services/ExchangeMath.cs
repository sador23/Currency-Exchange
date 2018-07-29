using CurrencyExchange.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class ExchangeMath
    {
        private readonly List<DailyCurrency> _rates;

        public ExchangeMath(List<DailyCurrency> rates)
        {
            _rates = rates;
        }

        public decimal CalculateExchange(ExchangeInput exchangeInput)
        {
            var rateFrom = _rates.Where(x => x.Name.Equals(exchangeInput.From)).Select(x => x.Rate).FirstOrDefault();
            var rateTo = (decimal)_rates.Where(x => x.Name.Equals(exchangeInput.To)).Select(x => x.Rate).FirstOrDefault();
            var result = (decimal)(1.0 / rateFrom) * exchangeInput.Value * rateTo;
            return result;
        }

        public string GetDecimalFormattedString(decimal val, int precision)
        {
            string format = "0.";
            for(int i = 0; i < precision; i++)
            {
                format += "#";
            }
            return val.ToString(format);
        }
    }
}
