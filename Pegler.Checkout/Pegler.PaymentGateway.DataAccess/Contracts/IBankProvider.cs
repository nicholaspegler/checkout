using Pegler.PaymentGateway.DataAccess.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.DataAccess.Contracts
{
    public interface IBankProvider
    {
        Task<BankDto> GetAsync();
    }
}
