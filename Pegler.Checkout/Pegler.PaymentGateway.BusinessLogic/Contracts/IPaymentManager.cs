using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using System;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.BusinessLogic.Contracts
{
    public interface IPaymentManager
    {
        Task<(PaymentRespModel, ModelStateDictionary)> GetAsync(Guid paymentId, ModelStateDictionary modelStateDictionary);

        Task<(PaymentReqRespModel, ModelStateDictionary)> PostAsync(PaymentReqModel paymentReqModel, ModelStateDictionary modelStateDictionary);
    }
}
