using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using System;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Contracts
{
    public interface IPaymentManager
    {
        Task<(PaymentRespModel, string)> GetAsync(Guid paymentId);

        Task<(PaymentReqRespModel, string)> PostAsync(PaymentReqModel paymentReqModel);
    }
}
