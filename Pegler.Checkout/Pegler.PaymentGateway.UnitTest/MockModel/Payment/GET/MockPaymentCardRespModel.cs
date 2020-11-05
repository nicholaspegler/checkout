using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.GET
{
    public static class MockPaymentCardRespModel
    {
        public static PaymentCardRespModel Get()
        {
            return new PaymentCardRespModel()
            {
                NameOnCard = "J Doe",
                CardType = CardType.Credit.ToString(),
                Issuer = Issuer.Visa.ToString(),
                Cardnumber = "4485236273376331",
                Cvv = "123",
                ExpiryMonth = 1,
                ExpiryYear = 2020
            };
        }
    }
}
