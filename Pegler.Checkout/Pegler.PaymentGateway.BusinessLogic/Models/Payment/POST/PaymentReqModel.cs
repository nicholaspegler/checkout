namespace Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST
{
    public class PaymentReqModel
    {
        public string Currency { get; set; }

        public double Amount { get; set; }

        public PaymentCardReqModel CardDetails { get; set; }

        public PaymentRecipientReqModel RecipientDetails { get; set; }
    }
}
