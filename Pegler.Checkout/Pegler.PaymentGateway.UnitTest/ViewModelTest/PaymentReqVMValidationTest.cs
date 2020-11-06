using NUnit.Framework;
using Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST;
using Pegler.PaymentGateway.ViewModels.Payment.POST;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Pegler.PaymentGateway.UnitTest.ViewModelTest
{
    [TestFixture]
    public class PaymentReqVMValidationTest
    {
        [Test]
        public void PaymentReqVM_ShouldReturnErrorIfValuesAreNull()
        {
            // Arrange

            PaymentReqVM paymentReqVM = new PaymentReqVM();

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentReqVM, new ValidationContext(paymentReqVM), validationResults, true);

            ValidationResult currencyErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentReqVM.Currency));

            ValidationResult amountErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentReqVM.Amount));

            ValidationResult cardDetailsErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentReqVM.CardDetails));

            ValidationResult recipientDetailsErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentReqVM.RecipientDetails));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(4, validationResults.Count);
            Assert.AreEqual("The Currency field is required.", currencyErrorMessage.ErrorMessage);
            Assert.AreEqual("The Amount field is required.", amountErrorMessage.ErrorMessage);
            Assert.AreEqual("The CardDetails field is required.", cardDetailsErrorMessage.ErrorMessage);
            Assert.AreEqual("The RecipientDetails field is required.", recipientDetailsErrorMessage.ErrorMessage);
        }

        [Test]
        public void PaymentReqVM_ShouldReturnErrorIfAmountIsNegative()
        {
            // Arrange

            PaymentReqVM paymentReqVM = MockPaymentReqVM.Get();
            paymentReqVM.Amount = -1;

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentReqVM, new ValidationContext(paymentReqVM), validationResults, true);

            ValidationResult amountErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentReqVM.Amount));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Amount field must be greater then zero.", amountErrorMessage.ErrorMessage);
        }

        [Test]
        public void PaymentReqVM_ShouldReturnErrorIfAmountIsZero()
        {
            // Arrange

            PaymentReqVM paymentReqVM = MockPaymentReqVM.Get();
            paymentReqVM.Amount = 0;

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentReqVM, new ValidationContext(paymentReqVM), validationResults, true);

            ValidationResult amountErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentReqVM.Amount));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Amount field must be greater then zero.", amountErrorMessage.ErrorMessage);
        }
    }
}
