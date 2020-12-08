using Pegler.PaymentGateway.BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Pegler.PaymentGateway.ViewModels.Payment.POST
{
    public class PaymentCardReqVM : IValidatableObject
    {
        [Required]
        public string NameOnCard { get; set; }

        [Required]
        public CardType? CardType { get; set; }

        [Required]
        public Issuer? Issuer { get; set; }

        // a full Luhn Formula could be used, though for this purpose this should be alright
        [Required, CreditCard(ErrorMessage = "The Cardnumber field is not a valid card number.")]
        public string Cardnumber { get; set; }

        [Required]
        public string Cvv { get; set; }

        [Required, Range(1, 12)]
        public int? ExpiryMonth { get; set; }

        [Required]
        public int? ExpiryYear { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Cvv))
            {
                if (Issuer == BusinessLogic.Enums.Issuer.Amex 
                    && !Regex.IsMatch(Cvv, @"^\d{3,4}$"))
                {
                    yield return
                        new ValidationResult("The Cvv field must be 3 or 4 digits for Amex Cards.", new[] { "Cvv" });
                }
                else if (!Regex.IsMatch(Cvv, @"^\d{3}$"))
                {
                    yield return
                        new ValidationResult("The Cvv field must be 3 digits.", new[] { "Cvv" });
                }
            }

            if (ExpiryYear.HasValue && ExpiryMonth.HasValue && 
                DateTime.Compare(new DateTime(ExpiryYear.Value, ExpiryMonth.Value, 1), DateTime.UtcNow) < 0)
            {
                yield return
                    new ValidationResult("The ExpiryYear and ExpiryMonth may not be in the past.", new[] { "ExpiryYear", "ExpiryMonth" });
            }
        }
    }
}
