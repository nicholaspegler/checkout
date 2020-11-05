﻿using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Options;
using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Managers
{
    public class HttpClientManager : IHttpClientManager
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<AuthenticationOptions> authenticationOptions;

        public HttpClientManager(IHttpClientFactory httpClientFactory,
                                 IOptions<AuthenticationOptions> authenticationOptions)
        {
            this.httpClientFactory = httpClientFactory;
            this.authenticationOptions = authenticationOptions;
        }

        public async Task<(T, string)> GetAsync<T>(string path)
        {
            try
            {
                using (HttpClient httpClient = await GetHttpClientAsync())
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

        public async Task<(T, string)> PostAsync<T>(string path, StringContent stringContent)
        {
            try
            {
                using (HttpClient httpClient = await GetHttpClientAsync())
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

        private async Task<HttpClient> GetHttpClientAsync()
        {
            HttpClient httpClient = httpClientFactory.CreateClient("default");

            if (authenticationOptions?.Value.IsRequired == true)
            {
                // request / generate a token and add to the httpClient as required
            }

            return httpClient;
        }
    }
}
