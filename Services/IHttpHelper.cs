using System.IO;
using System.Threading.Tasks;

namespace CurrencyExchange.Services
{
    public interface IHttpHelper
    {
          Task<Stream> getResponseStreamAsync();
    }
}
