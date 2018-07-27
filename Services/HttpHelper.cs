using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace CurrencyExchange.Services
{
    public class HttpHelper : IHttpHelper
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly String URI = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";

        public async Task<Stream> getResponseStreamAsync()
        {
            var response = await client.GetAsync(this.URI);
            var stream = await response.Content.ReadAsStreamAsync();
            return stream;
        }
    }
}
