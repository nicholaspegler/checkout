namespace Pegler.PaymentGateway.BusinessLogic.Options
{
    public class AuthenticationOptions
    {
        public const string OptionKey = "Payment:Bank:Authentication";

        public bool IsRequired { get; set; }

        public string Url { get; set; }

        public string Key { get; set; }

        public string Secret { get; set; }
    }
}
