using Newtonsoft.Json;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using Pegler.PaymentGateway.DataAccess.Contracts;
using Pegler.PaymentGateway.DataAccess.Dtos;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IHttpClientManager httpClientManager;
        private readonly IBankProvider bankProvider;

        public PaymentManager(IHttpClientManager httpClientManager,
                              IBankProvider bankProvider)
        {
            this.httpClientManager = httpClientManager;
            this.bankProvider = bankProvider;
        }

        public async Task<(PaymentRespModel, string)> GetAsync(Guid paymentId)
        {
            // Before retrieving the bank details, either a lookup against sortcode or even using the sortcode as an id
            // would need to be done here to get the bank

            BankDto bankDto = await bankProvider.GetAsync();

            if (bankDto != null)
            {
                if (bankDto.Urls?.Any(u => u.Type == "Get") == true)
                {
                    string path = bankDto.Urls
                                         .Where(w => w.Type == "Get")
                                         .Select(s => s.Value)
                                         .FirstOrDefault();

                    return await httpClientManager.GetAsync<PaymentRespModel>(path, bankDto.Authentication);
                }

                Log.Information("Failed to retrieve GET Url for - {bankId}");
            }

            Log.Information("Failed to retrieve bank details for - {bankId}");

            return (null, "Failed to retrieve bank details");
        }

        public async Task<(PaymentReqRespModel, string)> PostAsync(PaymentReqModel paymentReqModel)
        {
            // Before retrieving the bank details, either a lookup against sortcode or even using the sortcode as an id
            // would need to be done here to get the bank

            BankDto bankDto = await bankProvider.GetAsync();

            if (bankDto != null)
            {
                if (bankDto.Urls?.Any(u => u.Type == "Post") == true)
                {
                    string path = bankDto.Urls
                                         .Where(w => w.Type == "Post")
                                         .Select(s => s.Value)
                                         .FirstOrDefault();

                    string paymentReqModelAsString = JsonConvert.SerializeObject(paymentReqModel);

                    StringContent stringContent = new StringContent(paymentReqModelAsString, Encoding.UTF8, "application/json");

                    return await httpClientManager.PostAsync<PaymentReqRespModel>(path, stringContent, bankDto.Authentication);
                }

                Log.Information("Failed to retrieve GET Url for - {bankId}");
            }

            Log.Information("Failed to retrieve bank details for - {bankId}");

            return (null, "Failed to retrieve bank details");
        }
    }
}
