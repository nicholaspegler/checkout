using System;

namespace Pegler.PaymentGateway.ViewModels.Payment.GET
{
    public class PaymentRespVM
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public string Currency { get; set; }

        public double Amount { get; set; }

        public PaymentCardRespVM CardDetails { get; set; }
    }
}
