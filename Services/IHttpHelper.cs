using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public interface IHttpHelper
    {
          Task<Stream> getResponseStreamAsync();
    }
}
