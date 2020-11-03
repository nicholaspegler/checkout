using Pegler.Bank.Enums;

namespace Pegler.Bank.ViewModels.Bank.GET
{
    public class BankCardRespVM
    {
        public string NameOnCard { get; set; }

        public CardType CardType { get; set; }

        public Issuer Issuer { get; set; }

        public string Cardnumber { get; set; }

        public string Cvv { get; set; }

        public int? ExpiryMonth { get; set; }

        public int? ExpiryYear { get; set; }
    }
}
