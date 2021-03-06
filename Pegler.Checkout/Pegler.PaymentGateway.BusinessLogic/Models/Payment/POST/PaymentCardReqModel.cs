﻿namespace Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST
{
    public class PaymentCardReqModel
    {
        public string NameOnCard { get; set; }

        public string CardType { get; set; }

        public string Issuer { get; set; }

        public string Cardnumber { get; set; }

        public string Cvv { get; set; }

        public int ExpiryMonth { get; set; }

        public int ExpiryYear { get; set; }
    }
}
