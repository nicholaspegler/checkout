using System;

namespace Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST
{
    public class PaymentReqRespModel
    {
        public Guid Id { get; set; }

        public string Status { get; set; }

        public string Href { get; set; }
    }
}
