using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public HomeController(IHttpHelper helper, ILogger<HomeController> logger, XMLParser xMLParser)
        {
            _httpHelper = helper;
            _logger = logger;
            _xmlParser = xMLParser;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpHelper.getResponseStreamAsync();
            var result = _xmlParser.StreamParser(response);
            return Json(result);
        }
    }
}
