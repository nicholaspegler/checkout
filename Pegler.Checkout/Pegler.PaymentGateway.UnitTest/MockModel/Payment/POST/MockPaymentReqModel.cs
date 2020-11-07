using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST
{
    public static class MockPaymentReqModel
    {
        public static PaymentReqModel Get()
        {
            return new PaymentReqModel()
            {
                Currency = CurrencyCode.GBP.ToString(),
                Amount = 1,
                CardDetails = MockPaymentCardReqModel.Get(),
                RecipientDetails = MockPaymentRecipientReqModel.Get()
            };
        }
    }
}
