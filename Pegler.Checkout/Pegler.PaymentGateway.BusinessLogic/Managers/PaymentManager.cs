using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using Pegler.PaymentGateway.BusinessLogic.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IHttpClientManager httpClientManager;
        private readonly IOptions<EndpointOptions> endpointOptions;

        private static string _Failed_ToGetPayment = "Failed to retrieve payment details.";
        private static string _Failed_ToPostPayment = "Failed to post payment details.";

        public PaymentManager(IHttpClientManager httpClientManager,
                              IOptions<EndpointOptions> endpointOptions)
        {
            this.httpClientManager = httpClientManager;
            this.endpointOptions = endpointOptions;
        }

        public async Task<(PaymentRespModel, ModelStateDictionary)> GetAsync(Guid paymentId, ModelStateDictionary modelStateDictionary)
        {
            (PaymentRespModel paymentRespModel, string errorMessage) = await httpClientManager.GetAsync<PaymentRespModel>($"{endpointOptions?.Value.Endpoint}{paymentId}");

            if (!string.IsNullOrEmpty(errorMessage))
            {
                modelStateDictionary.AddModelError("Payment", _Failed_ToGetPayment);
            }

            return (paymentRespModel, modelStateDictionary);
        }

        public async Task<(PaymentReqRespModel, ModelStateDictionary)> PostAsync(PaymentReqModel paymentReqModel, ModelStateDictionary modelStateDictionary)
        {
            string paymentReqModelAsString = JsonConvert.SerializeObject(paymentReqModel);

            StringContent stringContent = new StringContent(paymentReqModelAsString, Encoding.UTF8, "application/json");

            (PaymentReqRespModel paymentReqRespModel, string errorMessage) = await httpClientManager.PostAsync<PaymentReqRespModel>(endpointOptions?.Value.Endpoint, stringContent);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                modelStateDictionary.AddModelError("Payment", _Failed_ToPostPayment);
            }

            return (paymentReqRespModel, modelStateDictionary);
        }
    }
}
