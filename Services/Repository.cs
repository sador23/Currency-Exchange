using CurrencyExchange.Context;
using CurrencyExchange.DTO;
using CurrencyExchange.Models;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> SaveChangesAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public ICollection<Currency> GetCurrencies()
        {
            return _context.Currency.ToList();
        }

        private DateTime GetLatestDate()
        {
            var now = DateTime.Now.Date;
            var counter = -1;
            while (!IsDateSaved(now))
            {
                now = DateTime.Now.AddDays(counter).Date;
                counter--;
                if (counter < -110) throw new Exception("Something went wrong");
            }
            return now;
        }

        public List<DailyCurrency> GetTodaysRates()
        {
            var now = GetLatestDate();
            var result = _context.DailyRate.Where(x => x.Date.Equals(now)).Include(x => x.Currencies).ThenInclude(x => x.Currency).ToList();

            List<DailyCurrency> rates = new List<DailyCurrency>();
            foreach (var item in result)
            {
                foreach(var curr in item.Currencies.Select(x => new {Rate = x.Rate, Name = x.Currency.Name }))
                {
                    DailyCurrency dailyRate = new DailyCurrency()
                    {
                        Name = curr.Name,
                        Rate = curr.Rate
                    };
                    rates.Add(dailyRate);
                }
            }
            return rates;
        }

        /**
        * AddCurrencyAsync
        * If the Country is not saved to the database,
        * it shall save it. 
        * Shall return the object from the table with the given name
        * <param name="name">The name of the country</param>
        **/
        public async Task<Currency> AddCurrencyAsync(string name)
        {
            bool isSaved = _context.Currency.AsNoTracking().Any(x => x.Name.Equals(name));
            if (!isSaved)
            {
                var curr = new Currency()
                {
                    Name = name
                };
                _context.Currency.Add(curr);
                await SaveChangesAsync();
                return curr;
            }
            var currency = await _context.Currency.FirstOrDefaultAsync(x => x.Name.Equals(name));
            return currency;
        }

        /**
        * AddDateAsync
        * If the Date is not saved to the database,
        * it shall save it. 
        * Shall return the object from the table with the given date
        * <param name="date">The date to be saved</param>
        **/
        public async Task<DailyRate> AddDateAsync(DateTime date)
        {
            bool isSaved = IsDateSaved(date);
            if (!isSaved)
            {
                var newDate = new DailyRate()
                {
                    Date = date.Date
                };
                _context.DailyRate.Add(newDate);
                await SaveChangesAsync();
                return newDate;
            }
            return await _context.DailyRate.FirstOrDefaultAsync(x => x.Date.Date.Equals(date.Date));
        }

        public bool IsDateSaved(DateTime date)
        {
            bool isSaved = _context.DailyRate.AsNoTracking().Any(x => x.Date.Date.Equals(date.Date));
            return isSaved;
        }

        /**
        * GetLastSavedDate
        * Returns the latest saved date, and provides a fallback value
        * if none is saved
        **/
        public async Task<DateTime> GetLastSavedDate()
        {
            await _context.Database.EnsureCreatedAsync();
            var item = _context.DailyRate.OrderByDescending(x => x.Date).FirstOrDefault();
            if (item == null) return DateTime.Now.AddDays(-200);
            return item.Date.Date;
        }

        public async Task SeedDatabase(Dictionary<DateTime, Dictionary<string,double>> data)
        {
            foreach (var item in data)
            {
                var readDate = await AddDateAsync(item.Key);
            }
            foreach(var item in data)
            {
                var readDate = await AddDateAsync(item.Key);
                await SaveNewDay(item.Value, item.Key, readDate);
            }
        }

        public StatisticHelper GetStatisticByCountry(string name)
        {
            StatisticHelper statisticHelper = new StatisticHelper();
            Dictionary<DateTime, decimal> rates = new Dictionary<DateTime, decimal>();
            statisticHelper.rates = rates;
            statisticHelper.Name = name;
            var result = _context.Currency.Where(x => x.Name.Equals(name)).Include(x => x.DailyRates).ThenInclude(x => x.DailyRate).FirstOrDefault();
            foreach (var item in result.DailyRates)
            {
                rates.Add(item.DailyRate.Date.Date, (decimal)item.Rate);
            }
            return statisticHelper;

        }

        public async Task SaveNewDay(Dictionary<string, double> rates, DateTime date, DailyRate dailyRate)
        {
            foreach(var item in rates)
            {
                var currency = await AddCurrencyAsync(item.Key);
                Composite composite = new Composite()
                {
                    Currency = currency,
                    DailyRate = dailyRate,
                    Rate = item.Value
                };
                _context.Add(composite);
            }
            await SaveChangesAsync();
        }

        public async Task DeleteAll()
        {
            var composites = _context.DailyRate;
            _context.RemoveRange(composites);
            await _context.SaveChangesAsync();
        }
    }
}
