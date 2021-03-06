﻿using System.ComponentModel.DataAnnotations;

namespace Pegler.PaymentGateway.ViewModels.Payment.POST
{
    public class PaymentRecipientReqVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SortCode { get; set; }

        [Required]
        public string Accountnumber { get; set; }

        public string PaymentRefernce { get; set; }
    }
}
