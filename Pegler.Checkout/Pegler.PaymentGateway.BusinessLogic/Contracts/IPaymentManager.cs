using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Contracts
{
    public interface IPaymentManager
    {
        Task<(PaymentRespModel, string)> GetAsync(Guid paymentId);

        Task<(string, string)> PostAsync();

    }
}
