using System;

namespace Pegler.PaymentGateway.ViewModels.Payment.GET
{
    public class PaymentCardRespVM
    {
        public string NameOnCard { get; set; }

        public string CardType { get; set; }

        public string Issuer { get; set; }

        public string CardnumberLast4 { get; set; }

        public string Cvv { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}
