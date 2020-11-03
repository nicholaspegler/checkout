using System;

namespace Pegler.PaymentGateway.DataAccess.Dtos
{
    public class BaseDto
    {
        public DateTime DateCreated { get; set; }

        public DateTime? DateUpdated { get; set; }

        public bool Deleted { get; set; }
    }
}
