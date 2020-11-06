using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using System;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST
{
    public static class MockPaymentReqRespModel
    {
        public static PaymentReqRespModel Get(Guid id, PaymentStatus paymentStatus)
        {
            return new PaymentReqRespModel()
            {
                Id = id,
                Status = paymentStatus.ToString(),
                Href = $"/api/v1/Bank/{id}"
            };
        }
    }
}
