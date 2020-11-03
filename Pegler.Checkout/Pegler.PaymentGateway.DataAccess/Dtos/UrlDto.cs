using System;

namespace Pegler.PaymentGateway.DataAccess.Dtos
{
    public class UrlDto
    {
        public Guid Id { get; set; }

        public Guid BankId { get; set; }

        // This would normally be an int with a foreignKey to a lookup table reference
        public string Type { get; set; }

        public string Value { get; set; }
    }
}
