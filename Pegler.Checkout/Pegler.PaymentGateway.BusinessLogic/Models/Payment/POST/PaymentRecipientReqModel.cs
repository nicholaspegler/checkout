namespace Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST
{
    public class PaymentRecipientReqModel
    {
        public string Name { get; set; }

        public string SortCode { get; set; }

        public string Accountnumber { get; set; }

        public string PaymentRefernce { get; set; }
    }
}
