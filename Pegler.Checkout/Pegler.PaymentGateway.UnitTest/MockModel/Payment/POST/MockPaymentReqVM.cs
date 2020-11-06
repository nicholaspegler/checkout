using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.ViewModels.Payment.POST;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST
{
    public static class MockPaymentReqVM
    {
        public static PaymentReqVM Get()
        {
            return new PaymentReqVM()
            {
                Currency = CurrencyCode.GBP,
                Amount = 1,
                CardDetails = MockPaymentCardReqVM.Get(),
                RecipientDetails = MockPaymentRecipientReqVM.Get()
            };
        }
    }
}
