using System.ComponentModel.DataAnnotations;

namespace Pegler.Bank.ViewModels.Bank.POST
{
    public class BankRecipientReqVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SortCode { get; set; }

        [Required]
        public string Accountnumber { get; set; }

        public string PaymentRefernce { get; set; }
    }
}
