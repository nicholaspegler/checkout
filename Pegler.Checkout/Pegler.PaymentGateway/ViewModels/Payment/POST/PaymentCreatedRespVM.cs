using Pegler.PaymentGateway.ViewModels.Common.POST;

namespace Pegler.PaymentGateway.ViewModels.Payment.POST
{
    public class PaymentCreatedRespVM : CreatedRespVM
    {
        public string Status { get; set; }
    }
}
