using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using NUnit.Framework;
using Pegler.PaymentGateway.AutoMapperMapping;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.Controllers;
using Pegler.PaymentGateway.UnitTest.MockModel.Payment.GET;
using Pegler.PaymentGateway.ViewModels.Payment.GET;
using System;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.UnitTest.ControllerTest
{
    [TestFixture]
    public class PaymentTest
    {
        private IMapper mapper = null;
        private Mock<IPaymentManager> mockPaymentManager = null;

        private PaymentController paymentController = null;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new Mappings());
            });

            mapper = mockMapper.CreateMapper();

            mockPaymentManager = new Mock<IPaymentManager>();

            paymentController = new PaymentController(mapper,
                                                      mockPaymentManager.Object);
        }

        [TearDown]
        public void TearDown()
        {
            mapper = null;
            mockPaymentManager = null;

            paymentController = null;
        }

        #region GET

        [Test]
        public async Task GetPayment_ReturnsOkObjectResult()
        {
            // Arrange

            Guid id = Guid.NewGuid();
            PaymentStatus paymentStatus = PaymentStatus.Paid;

            PaymentRespModel paymentRespModel = MockPaymentRespModel.Get(id, paymentStatus);

            mockPaymentManager.Setup(s => s.GetAsync(It.IsAny<Guid>(), It.IsAny<ModelStateDictionary>()))
                              .Returns(Task.FromResult<(PaymentRespModel, ModelStateDictionary)>((paymentRespModel, new ModelStateDictionary())))
                              .Verifiable();

            // Act

            ActionResult actionResult = await paymentController.Get(id) as ActionResult;

            // Assert

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult);

            OkObjectResult okObjectResult = actionResult as OkObjectResult;

            Assert.IsNotNull(okObjectResult);
            Assert.AreEqual(StatusCodes.Status200OK, okObjectResult.StatusCode);

            Assert.IsNotNull(okObjectResult.Value);
            Assert.IsInstanceOf<PaymentRespVM>(okObjectResult.Value);

            PaymentRespVM paymentRespVM = okObjectResult.Value as PaymentRespVM;

            Assert.IsNotNull(paymentRespVM);
            Assert.AreEqual(id, paymentRespVM.Id);
            Assert.AreEqual(paymentStatus.ToString(), paymentRespVM.Status);
            Assert.AreEqual(CurrencyCode.GBP.ToString(), paymentRespVM.Currency);
            Assert.AreEqual(1, paymentRespVM.Amount);

            Assert.IsNotNull(paymentRespVM.CardDetails);
            Assert.AreEqual("J Doe", paymentRespVM.CardDetails.NameOnCard);
            Assert.AreEqual(CardType.Credit.ToString(), paymentRespVM.CardDetails.CardType);
            Assert.AreEqual(Issuer.Visa.ToString(), paymentRespVM.CardDetails.Issuer);
            Assert.AreEqual("6331", paymentRespVM.CardDetails.CardnumberLast4);
            Assert.AreEqual("***", paymentRespVM.CardDetails.Cvv);
            Assert.AreEqual(1, paymentRespVM.CardDetails.ExpiryMonth);
            Assert.AreEqual(2020, paymentRespVM.CardDetails.ExpiryYear);

            mockPaymentManager.Verify();
        }

        #endregion

        #region POST

        #endregion
    }
}
