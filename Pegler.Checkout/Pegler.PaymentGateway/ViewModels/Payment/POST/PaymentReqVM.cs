using Pegler.PaymentGateway.BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.ViewModels.Payment.POST
{
    public class PaymentReqVM
    {
        [Required]
        public CurrencyCode? Currency { get; set; }

        [Required]

        public double? Amount { get; set; }

        [Required]
        public PaymentCardReqVM CardDetails { get; set; }

        [Required]
        public PaymentRecipientReqVM RecipientDetails { get; set; }
    }
}
