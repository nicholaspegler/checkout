using Pegler.PaymentGateway.ViewModels.Payment.POST;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST
{
    public static class MockPaymentRecipientReqVM
    {
        public static PaymentRecipientReqVM Get()
        {
            return new PaymentRecipientReqVM()
            {
                Name = "A Smith",
                SortCode = "040004",
                Accountnumber = "12345678",
                PaymentRefernce = "Mocked"
            };
        }
    }
}
