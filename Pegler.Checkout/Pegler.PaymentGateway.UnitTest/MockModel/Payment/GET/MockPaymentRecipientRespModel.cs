using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.GET
{
    public static class MockPaymentRecipientRespModel
    {
        public static PaymentRecipientRespModel Get()
        {
            return new PaymentRecipientRespModel()
            {
                Name = "A Smith",
                SortCode = "040004",
                Accountnumber = "12345678",
                PaymentRefernce = "Mocked"
            };
        }
    }
}
