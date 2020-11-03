using Pegler.Bank.Enums;
using System;

namespace Pegler.Bank.ViewModels.Bank.POST
{
    public class BankReqRespVM
    {
        public Guid Id { get; set; }

        public PaymentStatus Status { get; set; }

        public string Href { get; set; }
    }
}
