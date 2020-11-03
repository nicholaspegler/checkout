using Pegler.PaymentGateway.DataAccess.Dtos;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Contracts
{
    public interface IHttpClientManager
    {
        Task<(T, string)> GetAsync<T>(string path, AuthenticationDto authenticationDto = null);

        Task<(T, string)> PostAsync<T>(string path, StringContent stringContent, AuthenticationDto authenticationDto = null);
    }
}
