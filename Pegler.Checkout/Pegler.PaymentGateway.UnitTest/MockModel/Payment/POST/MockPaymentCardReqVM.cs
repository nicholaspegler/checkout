using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.ViewModels.Payment.POST;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST
{
    public static class MockPaymentCardReqVM
    {
        public static PaymentCardReqVM Get()
        {
            return new PaymentCardReqVM()
            {
                NameOnCard = "J Doe",
                CardType = CardType.Credit,
                Issuer = Issuer.Visa,
                Cardnumber = "4485236273376331",
                Cvv = "123",
                ExpiryMonth = 1,
                ExpiryYear = 2020
            };
        }
    }
}
