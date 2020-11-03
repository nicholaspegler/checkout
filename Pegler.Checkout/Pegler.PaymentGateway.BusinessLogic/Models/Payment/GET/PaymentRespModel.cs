using System;
using System.Collections.Generic;
using System.Text;

namespace Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET
{
    public class PaymentRespModel
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public string Currency { get; set; }

        public double Amount { get; set; }

        public PaymentCardRespModel CardDetails { get; set; }

        public PaymentRecipientRespModel RecipientDetails { get; set; }
    }
}
