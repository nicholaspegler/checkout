using System;

namespace Pegler.PaymentGateway.DataAccess.Dtos
{
    public class AuthenticationDto : BaseDto
    {
        public Guid Id { get; set; }

        public Guid BankId { get; set; }

        
    }
}
