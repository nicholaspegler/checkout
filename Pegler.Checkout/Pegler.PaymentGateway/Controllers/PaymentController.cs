using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using Pegler.PaymentGateway.ViewModels.Payment.GET;
using Pegler.PaymentGateway.ViewModels.Payment.POST;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

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
            (PaymentRespModel paymentRespModel, ModelStateDictionary modelStateDictionary) = await paymentManager.GetAsync(id, ModelState);

            if (!modelStateDictionary.IsValid)
            {
                return BadRequest(modelStateDictionary);
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
        /// <param name="paymentReqVM"></param>
        /// <returns></returns>
        [HttpPost]
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
