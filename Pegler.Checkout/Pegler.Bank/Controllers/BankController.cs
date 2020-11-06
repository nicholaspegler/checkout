using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pegler.Bank.Enums;
using Pegler.Bank.ViewModels.Bank.GET;
using Pegler.Bank.ViewModels.Bank.POST;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pegler.Bank.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BankRespVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Get(Guid id)
        {
            if (id == Guid.Empty)
            {
                ModelState.AddModelError("id","The id field is required and may not be an empty.");

                return BadRequest(ModelState);
            }

            DateTime dateTimeNowUtc = DateTime.UtcNow;

            BankRespVM bankRespVM = new BankRespVM()
            {
                Id = id,
                Currency = CurrencyCode.GBP,
                Amount = 10.50,
                CardDetails = new BankCardRespVM()
                {
                    NameOnCard = "J Doe",
                    CardType = CardType.Credit,
                    Issuer = Issuer.Visa,
                    Cardnumber = "4485236273376331",
                    Cvv = "123",
                    ExpiryMonth = dateTimeNowUtc.Month,
                    ExpiryYear = dateTimeNowUtc.Year + 1
                },
                RecipientDetails = new BankRecipientRespVM()
                {
                    Name = "A Smith",
                    SortCode = "040004",
                    Accountnumber = "12345678",
                    PaymentRefernce = "Mocked example"
                }
            };

            return Ok(bankRespVM);
        }

        [HttpPost]
        [ProducesResponseType(typeof(BankReqRespVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Post([Required] BankReqVM bankReqVM)
        {
            if (ModelState.IsValid)
            {
                Guid id = Guid.NewGuid();

                BankReqRespVM bankReqRespVM = new BankReqRespVM()
                {
                    Id = id,
                    Status = PaymentStatus.Paid,
                    Href = $"/api/v1/Bank/{id}"
                };

                return Created(bankReqRespVM.Href, bankReqRespVM);
            }

            return BadRequest(ModelState);
        }
    }
}
