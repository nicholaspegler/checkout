using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using Pegler.PaymentGateway.ViewModels.Payment.GET;
using Pegler.PaymentGateway.ViewModels.Payment.POST;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
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

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PaymentRespVM), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
        {
            (PaymentRespModel paymentRespModel, ModelStateDictionary modelStateDictionary) = await paymentManager.GetAsync(id, ModelState);

            if (!modelStateDictionary.IsValid)
            {
                return BadRequest(modelStateDictionary);
            }

            if (paymentRespModel == null)
            {
                return NotFound();
            }

            PaymentRespVM paymentRespVM = mapper.Map<PaymentRespModel, PaymentRespVM>(paymentRespModel);

            return Ok(paymentRespVM);
        }

        [HttpPost]
        [ProducesResponseType(typeof(PaymentCreatedRespVM), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(PaymentReqVM paymentReqVM)
        {
            if (ModelState.IsValid)
            {
                PaymentReqModel paymentReqModel = mapper.Map<PaymentReqVM, PaymentReqModel>(paymentReqVM);

                (PaymentReqRespModel paymentReqRespModel, ModelStateDictionary modelStateDictionary) = await paymentManager.PostAsync(paymentReqModel, ModelState);

                if (!modelStateDictionary.IsValid)
                {
                    return BadRequest(modelStateDictionary);
                }

                PaymentCreatedRespVM paymentCreatedRespVM = new PaymentCreatedRespVM()
                {
                    Id = paymentReqRespModel.Id,
                    Status = paymentReqRespModel.Status,
                    Href = $"/api/v1/Payment/{paymentReqRespModel.Id}"
                };

                return Created(paymentCreatedRespVM.Href, paymentCreatedRespVM);
            }

            return BadRequest(ModelState);
        }

    }
}
