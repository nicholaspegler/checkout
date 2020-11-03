using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using Pegler.PaymentGateway.ViewModels.Payment.GET;
using Pegler.PaymentGateway.ViewModels.Payment.POST;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pegler.PaymentGateway.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPaymentManager paymentManager;

        public PaymentController(IMapper mapper,
                                 IPaymentManager paymentManager)
        {
            this.mapper = mapper;
            this.paymentManager = paymentManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            (PaymentRespModel paymentRespModel, string errorMessage) = await paymentManager.GetAsync(id);

            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest($"Failed to retrieve payment details");
            }

            if (paymentRespModel == null)
            {
                return Ok();
            }

            PaymentRespVM paymentRespVM = mapper.Map<PaymentRespModel, PaymentRespVM>(paymentRespModel);

            return Ok(paymentRespVM);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        [HttpPost]
        public async Task<IActionResult> Post([Required] PaymentReqVM paymentReqVM)
        {
            if (ModelState.IsValid)
            {
                PaymentReqModel paymentReqModel = new PaymentReqModel();


                (PaymentReqRespModel paymentReqRespModel, string errorMessage) = await paymentManager.PostAsync(paymentReqModel);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    return BadRequest($"Failed to process payment");
                }

                if (paymentReqRespModel == null)
                {
                    return BadRequest("Failed to process payment");
                }

                Guid id = Guid.NewGuid();

                PaymentCreatedRespVM paymentCreatedRespVM = new PaymentCreatedRespVM()
                {
                    Id = id,
                    Status = "Approved",
                    Href = $"/api/v1/Payment/{id}"
                };

                return Created(paymentCreatedRespVM.Href, paymentCreatedRespVM);

            }

            return StatusCode(StatusCodes.Status400BadRequest, ModelState);
        }

    }
}
