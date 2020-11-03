using System;
using System.Collections.Generic;

namespace Pegler.PaymentGateway.DataAccess.Dtos
{
    public class BankDto : BaseDto
    {
        public Guid Id { get; set; }

        public virtual AuthenticationDto Authentication { get; set; }

        public virtual IEnumerable<UrlDto> Urls { get; set; }
    }
}
