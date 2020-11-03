using System;

namespace Pegler.PaymentGateway.DataAccess.Dtos
{
    public class AuthenticationDto : BaseDto
    {
        public Guid Id { get; set; }

        public Guid BankId { get; set; }

        public bool IsRequired { get; set; }

        public string Url { get; set; }

        public string Key { get; set; }

        public string Secret { get; set; }
    }
}
