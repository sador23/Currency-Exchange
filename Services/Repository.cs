using CurrencyExchange.Context;
using CurrencyExchange.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public class Repository
    {
        private readonly ExchangerContext _context;

        public Repository(ExchangerContext context)
        {
            _context = context;
        }

        public async Task<Currency> GetCurrencyById(int id)
        {
            var currency = await _context.Currency.FindAsync(id);
            return currency;
        }

        public async Task<DailyRate> GetDailyRateById(int id)
        {
            var rate = await _context.DailyRate.FindAsync(id);
            return rate;
        }

        public ICollection<Currency> GetCurrencies()
        {
            return _context.Currency.ToList();
        }

        public ICollection<DailyRate> GetDailyRates()
        {
            return _context.DailyRate.ToList();
        }

        public Dictionary<string,double> GetTodaysRates()
        {
            Dictionary<string, double> rates = new Dictionary<string, double>();
            var result = _context.DailyRate.Where(x => x.Date.Equals(DateTime.Now.Date));
            return rates;
        }

    }
}
