using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST
{
    public static class MockPaymentRecipientReqModel
    {
        public static PaymentRecipientReqModel Get()
        {
            return new PaymentRecipientReqModel()
            {
                Name = "A Smith",
                SortCode = "040004",
                Accountnumber = "12345678",
                PaymentRefernce = "Mocked"
            };
        }
    }
}
