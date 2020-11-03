using Pegler.Bank.Enums;
using System;

namespace Pegler.Bank.ViewModels.Bank.GET
{
    public class BankRespVM
    {
        public Guid Id { get; set; }

        public PaymentStatus Status { get; set; }

        public CurrencyCode Currency { get; set; }

        public double Amount { get; set; }

        public BankCardRespVM CardDetails { get; set; }

        public BankRecipientRespVM RecipientDetails { get; set; }
    }
}
