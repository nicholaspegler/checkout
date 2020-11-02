using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Managers
{
    public class PaymentManager : IPaymentManager
    {
        private readonly IHttpClientManager httpClientManager;

        public PaymentManager(IHttpClientManager httpClientManager)
        {
            this.httpClientManager = httpClientManager;
        }

        public async Task<(PaymentRespModel, string)> GetAsync(Guid paymentId)
        {
            string path = string.Empty;

            return await httpClientManager.GetAsync<PaymentRespModel>(path);
        }

        public async Task<(string, string)> PostAsync()
        {
            throw new NotImplementedException();
        }
    }
}
