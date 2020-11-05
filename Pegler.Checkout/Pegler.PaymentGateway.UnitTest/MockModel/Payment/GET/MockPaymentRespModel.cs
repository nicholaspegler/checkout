using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using System;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.GET
{
    public static class MockPaymentRespModel
    {
        public static PaymentRespModel Get(Guid id, PaymentStatus paymentStatus)
        {
            return new PaymentRespModel()
            {
                Id = id,
                Status = paymentStatus.ToString(),
                Currency = CurrencyCode.GBP.ToString(),
                Amount = 1,
                CardDetails = MockPaymentCardRespModel.Get(),
                RecipientDetails = MockPaymentRecipientRespModel.Get()
            };
        }
    }
}
