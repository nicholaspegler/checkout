using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using System;

namespace Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST
{
    public static class MockPaymentCardReqModel
    {
        public static PaymentCardReqModel Get()
        {
            DateTime dateTimeUtcNow = DateTime.UtcNow;

            return new PaymentCardReqModel()
            {
                NameOnCard = "J Doe",
                CardType = CardType.Credit.ToString(),
                Issuer = Issuer.Visa.ToString(),
                Cardnumber = "4485236273376331",
                Cvv = "123",
                ExpiryMonth = dateTimeUtcNow.Month,
                ExpiryYear = dateTimeUtcNow.Year + 1
            };
        }
    }
}
