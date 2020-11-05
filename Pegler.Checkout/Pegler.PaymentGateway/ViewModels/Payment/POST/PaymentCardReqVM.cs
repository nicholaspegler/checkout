﻿using Pegler.PaymentGateway.BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pegler.PaymentGateway.ViewModels.Payment.POST
{
    public class PaymentCardReqVM
    {
        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public CardType? CardType { get; set; }

        [Required]
        public Issuer? Issuer { get; set; }

        [Required]
        public string Cardnumber { get; set; }

        [Required]
        public string Cvv { get; set; }

        [Required]
        public int? ExpiryMonth { get; set; }

        [Required]
        public int? ExpiryYear { get; set; }
    }
}
