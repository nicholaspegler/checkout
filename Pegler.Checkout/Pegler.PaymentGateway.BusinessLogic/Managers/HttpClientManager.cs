using IdentityModel.Client;
using Newtonsoft.Json;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.DataAccess.Dtos;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Managers
{
    public class HttpClientManager : IHttpClientManager
    {
        private readonly IHttpClientFactory httpClientFactory;

        public HttpClientManager(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<(T, string)> GetAsync<T>(string path, AuthenticationDto authenticationDto = null)
        {
            try
            {
                using (HttpClient httpClient = await GetHttpClientAsync(authenticationDto))
                {
                    HttpResponseMessage getResponse = await httpClient.GetAsync(path);

                    string responseContent = await getResponse.Content.ReadAsStringAsync();

                    if (getResponse.IsSuccessStatusCode)
                    {
                        return (JsonConvert.DeserializeObject<T>(responseContent), null);
                    }
                    else
                    {
                        Log.Information($"GET - StatusCode: {getResponse.StatusCode} | Response: {responseContent}");

                        return (default, $"Get request was unsuccessful.");
                    }
                }
            }
            catch (Exception exception)
            {
                Log.ForContext("Type", "Error")
                   .ForContext("Exception", exception)
                   .Error(exception, exception.Message);

                return (default, $"GET - An exception has occured: {exception.Message}");
            }
        }

        public async Task<(T, string)> PostAsync<T>(string path, StringContent stringContent, AuthenticationDto authenticationDto = null)
        {
            try
            {
                using (HttpClient httpClient = await GetHttpClientAsync(authenticationDto))
                {
                    HttpResponseMessage postResponse = await httpClient.PostAsync(path, stringContent);

                    string responseContent = await postResponse.Content.ReadAsStringAsync();

                    if (postResponse.IsSuccessStatusCode)
                    {
                        return (JsonConvert.DeserializeObject<T>(responseContent), null);
                    }
                    else
                    {
                        Log.Information($"POST - StatusCode: {postResponse.StatusCode} | Response: {responseContent}");

                        return (default, $"POST request was unsuccessful.");
                    }
                }
            }
            catch (Exception exception)
            {
                Log.ForContext("Type", "Error")
                   .ForContext("Exception", exception)
                   .Error(exception, exception.Message);

                return (default, $"POST - An exception has occured: {exception.Message}");
            }
        }

        private async Task<HttpClient> GetHttpClientAsync(AuthenticationDto authenticationDto = null)
        {
            HttpClient httpClient = httpClientFactory.CreateClient("default");

            if (authenticationDto?.IsRequired == true)
            {
                // request / generate a token and add to the httpClient as required
            }

            return httpClient;
        }
    }
}
