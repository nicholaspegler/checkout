﻿using Pegler.PaymentGateway.BusinessLogic.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pegler.PaymentGateway.ViewModels.Payment.POST
{
    public class PaymentReqVM : IValidatableObject
    {
        [Required]
        public CurrencyCode? Currency { get; set; }

        [Required]

        public double? Amount { get; set; }

        [Required]
        public PaymentCardReqVM CardDetails { get; set; }

        [Required]
        public PaymentRecipientReqVM RecipientDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Amount <= 0)
            {
                yield return
                    new ValidationResult("The Amount field must be greater then zero.", new[] { "Amount" });
            }
        }
    }
}
