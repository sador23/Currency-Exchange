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
            var response = await _httpHelper.getResponseStreamAsync();
            var lastDate = _repository.GetLastSavedDate();
            var result = _xmlParser.StreamParser(response, lastDate);
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
        public IActionResult StatisticData(string id)
        {
            var result = _repository.GetStatisticByCountry(id);
            return Json(result);
        }


        public IActionResult List()
        {
            ViewData["currencies"] = _repository.GetTodaysRates();
            return View();

        }
        [HttpPost]
        public IActionResult CalculateExchange([FromBody]ExchangeInput exchange)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ExchangeMath math = new ExchangeMath(_repository.GetTodaysRates());
            var result = math.CalculateExchange(exchange);
            return Json(math.GetDecimalFormattedString(result,2));
        }
    }
}
