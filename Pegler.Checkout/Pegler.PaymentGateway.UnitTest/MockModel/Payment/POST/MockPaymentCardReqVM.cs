using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.ViewModels.Payment.POST;
using System;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST
{
    public static class MockPaymentCardReqVM
    {
        public static PaymentCardReqVM Get()
        {
            DateTime dateTimeUtcNow = DateTime.UtcNow;

            return new PaymentCardReqVM()
            {
                NameOnCard = "J Doe",
                CardType = CardType.Credit,
                Issuer = Issuer.Visa,
                Cardnumber = "4485236273376331",
                Cvv = "123",
                ExpiryMonth = dateTimeUtcNow.Month,
                ExpiryYear = dateTimeUtcNow.Year + 1
            };
        }
    }
}
