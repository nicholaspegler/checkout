using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using Pegler.PaymentGateway.BusinessLogic.Contracts;
using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.BusinessLogic.Managers;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.GET;
using Pegler.PaymentGateway.BusinessLogic.Models.Payment.POST;
using Pegler.PaymentGateway.BusinessLogic.Options;
using Pegler.PaymentGateway.UnitTest.MockModel.Payment.GET;
using Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pegler.PaymentGateway.UnitTest.ManagerTest
{
    [TestFixture]
    public class PaymentManagerTest
    {
        private Mock<IHttpClientManager> mockHttpClientManager = null;
        private Mock<IOptions<EndpointOptions>> mockEndpointOptions = null;

        private PaymentManager paymentManager = null;

        [SetUp]
        public void Setup()
        {
            mockHttpClientManager = new Mock<IHttpClientManager>();

            EndpointOptions endpointOptions = new EndpointOptions() { Endpoint = "https://localhost/api/v1/bank/" };

            mockEndpointOptions = new Mock<IOptions<EndpointOptions>>();
            mockEndpointOptions.Setup(s => s.Value)
                               .Returns(endpointOptions);

            paymentManager = new PaymentManager(mockHttpClientManager.Object,
                                                mockEndpointOptions.Object);
        }

        [TearDown]
        public void TearDown()
        {
            mockHttpClientManager = null;
            mockEndpointOptions = null;

            paymentManager = null;
        }

        #region GET

        [Test]
        public async Task GetAsync_ReturnsPaymentRespModel()
        {
            // Arrange

            Guid paymentId = Guid.NewGuid();
            PaymentStatus paymentStatus = PaymentStatus.Paid;

            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            PaymentRespModel paymentRespModel = MockPaymentRespModel.Get(paymentId, paymentStatus);

            mockHttpClientManager.Setup(s => s.GetAsync<PaymentRespModel>(It.IsAny<string>()))
                                 .Returns(Task.FromResult<(PaymentRespModel, string)>((paymentRespModel, null)))
                                 .Verifiable();

            // Act

            (PaymentRespModel paymentRespModelResp, ModelStateDictionary modelStateDictionaryResp) = await paymentManager.GetAsync(paymentId, modelStateDictionary);

            // Assert

            Assert.IsNotNull(paymentRespModelResp);

            Assert.AreEqual(paymentId, paymentRespModelResp.Id);
            Assert.AreEqual(paymentStatus.ToString(), paymentRespModelResp.Status);
            Assert.AreEqual(CurrencyCode.GBP.ToString(), paymentRespModelResp.Currency);
            Assert.AreEqual(1, paymentRespModelResp.Amount);

            Assert.IsNotNull(paymentRespModelResp.CardDetails);
            Assert.AreEqual("J Doe", paymentRespModelResp.CardDetails.NameOnCard);
            Assert.AreEqual(CardType.Credit.ToString(), paymentRespModelResp.CardDetails.CardType);
            Assert.AreEqual(Issuer.Visa.ToString(), paymentRespModelResp.CardDetails.Issuer);
            Assert.AreEqual("4485236273376331", paymentRespModelResp.CardDetails.Cardnumber);
            Assert.AreEqual("123", paymentRespModelResp.CardDetails.Cvv);
            Assert.AreEqual(1, paymentRespModelResp.CardDetails.ExpiryMonth);
            Assert.AreEqual(2020, paymentRespModelResp.CardDetails.ExpiryYear);

            Assert.IsNotNull(paymentRespModelResp.RecipientDetails);
            Assert.AreEqual("A Smith", paymentRespModelResp.RecipientDetails.Name);
            Assert.AreEqual("040004", paymentRespModelResp.RecipientDetails.SortCode);
            Assert.AreEqual("12345678", paymentRespModelResp.RecipientDetails.Accountnumber);
            Assert.AreEqual("Mocked", paymentRespModelResp.RecipientDetails.PaymentRefernce);

            Assert.IsNotNull(modelStateDictionaryResp);
            Assert.IsTrue(modelStateDictionaryResp.IsValid);

            mockHttpClientManager.Verify(v => v.GetAsync<PaymentRespModel>(It.IsAny<string>()));
        }

        [Test]
        public async Task GetAsync_ReturnsModelStateDictionary()
        {
            // Arrange

            Guid paymentId = Guid.NewGuid();

            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            string error = "GET request was unsuccessful.";

            mockHttpClientManager.Setup(s => s.GetAsync<PaymentRespModel>(It.IsAny<string>()))
                                 .Returns(Task.FromResult<(PaymentRespModel, string)>((default, error)))
                                 .Verifiable();

            // Act

            (PaymentRespModel paymentRespModelResp, ModelStateDictionary modelStateDictionaryResp) = await paymentManager.GetAsync(paymentId, modelStateDictionary);

            // Assert

            Assert.IsNull(paymentRespModelResp);

            Assert.IsNotNull(modelStateDictionaryResp);
            Assert.IsFalse(modelStateDictionaryResp.IsValid);

            Assert.AreEqual(1, modelStateDictionaryResp.ErrorCount);
            Assert.IsTrue(modelStateDictionaryResp.ContainsKey("Payment"));

            mockHttpClientManager.Verify(v => v.GetAsync<PaymentRespModel>(It.IsAny<string>()));
        }

        #endregion

        #region POST

        [Test]
        public async Task PostAsync_ReturnsPaymentReqRespModel()
        {
            // Arrange

            Guid paymentId = Guid.NewGuid();
            PaymentStatus paymentStatus = PaymentStatus.Paid;

            PaymentReqModel paymentReqModel = MockPaymentReqModel.Get();

            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            PaymentReqRespModel paymentReqRespModel = MockPaymentReqRespModel.Get(paymentId, paymentStatus);

            mockHttpClientManager.Setup(s => s.PostAsync<PaymentReqRespModel>(It.IsAny<string>(), It.IsAny<StringContent>()))
                                 .Returns(Task.FromResult<(PaymentReqRespModel, string)>((paymentReqRespModel, null)))
                                 .Verifiable();

            // Act

            (PaymentReqRespModel paymentReqRespModelResp, ModelStateDictionary modelStateDictionaryResp) = await paymentManager.PostAsync(paymentReqModel, modelStateDictionary);

            // Assert

            Assert.IsNotNull(paymentReqRespModelResp);

            Assert.AreEqual(paymentId, paymentReqRespModelResp.Id);
            Assert.AreEqual(paymentStatus.ToString(), paymentReqRespModelResp.Status);
            Assert.AreEqual($"/api/v1/Bank/{paymentId}", paymentReqRespModelResp.Href);

            Assert.IsNotNull(modelStateDictionaryResp);
            Assert.IsTrue(modelStateDictionaryResp.IsValid);

            mockHttpClientManager.Verify(v => v.PostAsync<PaymentReqRespModel>(It.IsAny<string>(), It.IsAny<StringContent>()));
        }

        [Test]
        public async Task PostAsync_ReturnsModelStateDictionary()
        {
            // Arrange

            PaymentReqModel paymentReqModel = MockPaymentReqModel.Get();

            ModelStateDictionary modelStateDictionary = new ModelStateDictionary();

            string error = "POST request was unsuccessful.";

            mockHttpClientManager.Setup(s => s.PostAsync<PaymentReqRespModel>(It.IsAny<string>(), It.IsAny<StringContent>()))
                                 .Returns(Task.FromResult<(PaymentReqRespModel, string)>((default, error)))
                                 .Verifiable();

            // Act

            (PaymentReqRespModel paymentReqRespModelResp, ModelStateDictionary modelStateDictionaryResp) = await paymentManager.PostAsync(paymentReqModel, modelStateDictionary);

            // Assert

            Assert.IsNull(paymentReqRespModelResp);

            Assert.IsNotNull(modelStateDictionaryResp);
            Assert.IsFalse(modelStateDictionaryResp.IsValid);

            Assert.AreEqual(1, modelStateDictionaryResp.ErrorCount);
            Assert.IsTrue(modelStateDictionaryResp.ContainsKey("Payment"));

            mockHttpClientManager.Verify(v => v.PostAsync<PaymentReqRespModel>(It.IsAny<string>(), It.IsAny<StringContent>()));
        }

        #endregion
    }
}
