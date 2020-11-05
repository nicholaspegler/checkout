using System.Net.Http;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Contracts
{
    public interface IHttpClientManager
    {
        Task<(T, string)> GetAsync<T>(string path);

        Task<(T, string)> PostAsync<T>(string path, StringContent stringContent);
    }
}
