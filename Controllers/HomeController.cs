using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CurrencyExchange.DTO;
using CurrencyExchange.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CurrencyExchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpHelper _httpHelper;
        private readonly ILogger<HomeController> _logger;
        private readonly XMLParser _xmlParser;
        private readonly Repository _repository;

        public HomeController(IHttpHelper helper, ILogger<HomeController> logger, XMLParser xMLParser,
                                Repository repository)
        {
            _httpHelper = helper;
            _logger = logger;
            _xmlParser = xMLParser;
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            //await _repository.DeleteAll();
            var response = await _httpHelper.getResponseStreamAsync();
            var lastDate = _repository.getLastSavedDate();
            var result = _xmlParser.StreamParser(response,lastDate);
            ViewData["currencies"] = _repository.GetTodaysRates();
            await _repository.SeedDatabase(result);
            return View();
        }

        public IActionResult Statistic()
        {
            ViewData["currencies"] = _repository.GetTodaysRates();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> StatisticData(string id)
        {
            try
            {
                var result = _repository.GetStatisticByCountry(id);
                return Json(result);
            }
            catch(Exception ex)
            {
                return Json(ex.StackTrace);
            }
        }


        public async Task<IActionResult> List()
        {
            //ExchangeMath math = new ExchangeMath(await _repository.GetTodayRatesDictionary());
            ViewData["currencies"] = _repository.GetTodaysRates();
            return View();

        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> CalculateExchange([FromBody]ExchangeInput exchange)
        {
            if (!ModelState.IsValid)
            {
                //return View(exchange);
                return BadRequest(ModelState);
            }
            ExchangeMath math = new ExchangeMath(await _repository.GetTodayRatesDictionary());
            return Json(math.CalculateExchange(exchange));
        }
    }
}
