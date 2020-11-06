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
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using Pegler.PaymentGateway.Controllers;
using Pegler.PaymentGateway.UnitTest.MockModel.Payment.GET;
using Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST;
using Pegler.PaymentGateway.ViewModels.Payment.GET;
using Pegler.PaymentGateway.ViewModels.Payment.POST;
using System;
using System.Linq;
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
            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            mockPaymentManager.Setup(s => s.GetAsync(It.IsAny<Guid>(), It.IsAny<ModelStateDictionary>()))
                              .Returns(Task.FromResult<(PaymentRespModel, ModelStateDictionary)>((paymentRespModel, modelStateDictionary)))
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

        [Test]
        public async Task GetPayment_ReturnsBadRequestObjectResult_WhenInvalidModelStateReturned()
        {
            // Arrange

            Guid id = Guid.NewGuid();

            PaymentRespModel paymentRespModel = null;

            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError("UnitTest", "Error");

            mockPaymentManager.Setup(s => s.GetAsync(It.IsAny<Guid>(), It.IsAny<ModelStateDictionary>()))
                              .Returns(Task.FromResult<(PaymentRespModel, ModelStateDictionary)>((paymentRespModel, modelStateDictionary)))
                              .Verifiable();

            // Act

            ActionResult actionResult = await paymentController.Get(id) as ActionResult;

            // Assert

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);

            BadRequestObjectResult badRequestObjectResult = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(badRequestObjectResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);

            Assert.IsNotNull(badRequestObjectResult.Value);
            Assert.IsInstanceOf<SerializableError>(badRequestObjectResult.Value);

            SerializableError serializableError = badRequestObjectResult.Value as SerializableError;

            Assert.IsNotNull(serializableError);

            Assert.AreEqual(1, serializableError.Keys.Count());
            Assert.IsTrue(serializableError.ContainsKey("UnitTest"));

            mockPaymentManager.Verify();
        }

        [Test]
        public async Task GetPayment_ReturnsOkResult_WhenPaymentNotFound()
        {
            // Arrange

            Guid id = Guid.NewGuid();

            PaymentRespModel paymentRespModel = null;
            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            mockPaymentManager.Setup(s => s.GetAsync(It.IsAny<Guid>(), It.IsAny<ModelStateDictionary>()))
                              .Returns(Task.FromResult<(PaymentRespModel, ModelStateDictionary)>((paymentRespModel, modelStateDictionary)))
                              .Verifiable();

            // Act

            ActionResult actionResult = await paymentController.Get(id) as ActionResult;

            // Assert

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<OkResult>(actionResult);

            OkResult okResult = actionResult as OkResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(StatusCodes.Status200OK, okResult.StatusCode);

            mockPaymentManager.Verify();
        }

        #endregion

        #region POST

        [Test]
        public async Task PostPayment_ReturnsCreatedResult()
        {
            // Arrange

            Guid id = Guid.NewGuid();
            PaymentStatus paymentStatus = PaymentStatus.Paid;

            PaymentReqVM paymentReqVM = MockPaymentReqVM.Get();

            PaymentReqRespModel paymentReqRespModel = MockPaymentReqRespModel.Get(id, paymentStatus);
            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            mockPaymentManager.Setup(s => s.PostAsync(It.IsAny<PaymentReqModel>(), It.IsAny<ModelStateDictionary>()))
                              .Returns(Task.FromResult<(PaymentReqRespModel, ModelStateDictionary)>((paymentReqRespModel, modelStateDictionary)))
                              .Verifiable();

            // Act

            ActionResult actionResult = await paymentController.Post(paymentReqVM) as ActionResult;

            // Assert

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<CreatedResult>(actionResult);

            CreatedResult createdResult = actionResult as CreatedResult;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual(StatusCodes.Status201Created, createdResult.StatusCode);

            Assert.IsNotNull(createdResult.Value);
            Assert.IsInstanceOf<PaymentCreatedRespVM>(createdResult.Value);

            PaymentCreatedRespVM paymentCreatedRespVM = createdResult.Value as PaymentCreatedRespVM;

            Assert.IsNotNull(paymentCreatedRespVM);
            Assert.AreEqual(id, paymentCreatedRespVM.Id);
            Assert.AreEqual(paymentStatus.ToString(), paymentCreatedRespVM.Status);
            Assert.AreEqual($"/api/v1/Payment/{id}", paymentCreatedRespVM.Href);

            mockPaymentManager.Verify();
        }

        [Test]
        public async Task PostPayment_ReturnsBadRequestObjectResult_WhenInvalidModelStateEntered()
        {
            // Arrange

            PaymentReqVM paymentReqVM = null;

            // Act

            ActionResult actionResult = await paymentController.Post(paymentReqVM) as ActionResult;

            // Assert

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);

            BadRequestObjectResult badRequestObjectResult = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(badRequestObjectResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);

            Assert.IsNotNull(badRequestObjectResult.Value);
            Assert.IsInstanceOf<SerializableError>(badRequestObjectResult.Value);

            SerializableError serializableError = badRequestObjectResult.Value as SerializableError;

            Assert.IsNotNull(serializableError);

            Assert.AreEqual(1, serializableError.Keys.Count());
            Assert.IsTrue(serializableError.ContainsKey("UnitTest"));

            mockPaymentManager.Verify();
        }

        [Test]
        public async Task PostPayment_ReturnsBadRequestObjectResult_WhenInvalidModelStateReturned()
        {
            // Arrange

            Guid id = Guid.NewGuid();
            PaymentStatus paymentStatus = PaymentStatus.Paid;

            PaymentReqVM paymentReqVM = MockPaymentReqVM.Get();

            PaymentReqRespModel paymentReqRespModel = MockPaymentReqRespModel.Get(id, paymentStatus);
            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();
            modelStateDictionary.AddModelError("UnitTest", "Error");


            mockPaymentManager.Setup(s => s.PostAsync(It.IsAny<PaymentReqModel>(), It.IsAny<ModelStateDictionary>()))
                              .Returns(Task.FromResult<(PaymentReqRespModel, ModelStateDictionary)>((paymentReqRespModel, modelStateDictionary)))
                              .Verifiable();

            // Act

            ActionResult actionResult = await paymentController.Post(paymentReqVM) as ActionResult;

            // Assert

            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOf<BadRequestObjectResult>(actionResult);

            BadRequestObjectResult badRequestObjectResult = actionResult as BadRequestObjectResult;

            Assert.IsNotNull(badRequestObjectResult);
            Assert.AreEqual(StatusCodes.Status400BadRequest, badRequestObjectResult.StatusCode);

            Assert.IsNotNull(badRequestObjectResult.Value);
            Assert.IsInstanceOf<SerializableError>(badRequestObjectResult.Value);

            SerializableError serializableError = badRequestObjectResult.Value as SerializableError;

            Assert.IsNotNull(serializableError);

            Assert.AreEqual(1, serializableError.Keys.Count());
            Assert.IsTrue(serializableError.ContainsKey("UnitTest"));

            mockPaymentManager.Verify();
        }



        #endregion
    }
}
