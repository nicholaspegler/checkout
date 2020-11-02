﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

        public PaymentController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok();
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

                Guid id = Guid.NewGuid();

                PaymentCreatedRespVM paymentCreatedRespVM = new PaymentCreatedRespVM()
                {
                    Id = id,
                    Status = "Approved",
                    Href = $"/api/v1/Payment/{id}"
                };

                return Created(paymentCreatedRespVM.Href, paymentCreatedRespVM);

            }

            return StatusCode(400, ModelState);
        }

    }
}
