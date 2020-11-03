using Pegler.Bank.Enums;
using System.ComponentModel.DataAnnotations;

namespace Pegler.Bank.ViewModels.Bank.POST
{
    public class BankReqVM
    {
        [Required]
        public CurrencyCode? Currency { get; set; }

        [Required]
        public double? Amount { get; set; }

        [Required]
        public BankCardReqVM CardDetails { get; set; }

        [Required]
        public BankRecipientReqVM RecipientDetails { get; set; }
    }
}
