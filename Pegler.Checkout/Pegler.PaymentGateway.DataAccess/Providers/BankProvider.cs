using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pegler.PaymentGateway.DataAccess.Contracts;
using Pegler.PaymentGateway.DataAccess.Dtos;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.DataAccess.Providers
{
    public class BankProvider : IBankProvider
    {
        private readonly IConfiguration configuration;

        public BankProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // A bankId would be used here to return the correct details
        // In this case we are only using a single bank that is configured in appsettings
        public async Task<BankDto> GetAsync()
        {
            // Normally this data would be stored in a database and accessed via a db context (MS Sql and Microsoft.EntityFrameworkCore)
            // I am aware that for this instance alternative data storage maybe better suited.

            //string bank = configuration.GetSection("Payment")?
            //                           .GetSection("Bank")?
            //                           .Get();


            var foo = configuration.GetSection("Payment");

            var bar = configuration.GetSection("Payment")?
                                   .GetSection("Bank");

            string a = configuration.GetSection("Payment:Bank").Value;


            string getUrl = "";
            string postUrl = "";

            string bank = $"{{\"Authentication\":{{\"IsRequired\":\"false\",\"Url\":\"\",\"Key\":\"\",\"Secret\":\"\"}},\"Urls\":[{{\"Type\":\"Get\",\"Value\":\"{getUrl}\"}},{{\"Type\":\"Post\",\"Value\":\"{postUrl}\"}}]}}";

            if (!string.IsNullOrEmpty(bank))
            {
                BankDto bankDto = JsonConvert.DeserializeObject<BankDto>(bank);

                return bankDto;
            }

            return null;
        }
    }
}
