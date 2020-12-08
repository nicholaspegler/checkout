using NUnit.Framework;
using Pegler.PaymentGateway.BusinessLogic.Enums;
using Pegler.PaymentGateway.UnitTest.MockModel.Payment.POST;
using Pegler.PaymentGateway.ViewModels.Payment.POST;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Pegler.PaymentGateway.UnitTest.ViewModelTest
{
    [TestFixture]
    public class PaymentCardReqVMValidationTest
    {
        [Test]
        public void PaymentCardReqVM_ShouldReturnErrorIfValuesAreNull()
        {
            // Arrange

            PaymentCardReqVM paymentCardReqVM = new PaymentCardReqVM();

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentCardReqVM, new ValidationContext(paymentCardReqVM), validationResults, true);

            ValidationResult nameOnCardErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.NameOnCard));

            ValidationResult cardTypeErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.CardType));

            ValidationResult issuerErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.Issuer));

            ValidationResult cardnumberErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.Cardnumber));

            ValidationResult cvvErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.Cvv));

            ValidationResult expiryMonthErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.ExpiryMonth));

            ValidationResult expiryYearErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.ExpiryYear));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(7, validationResults.Count);
            Assert.AreEqual("The NameOnCard field is required.", nameOnCardErrorMessage.ErrorMessage);
            Assert.AreEqual("The CardType field is required.", cardTypeErrorMessage.ErrorMessage);
            Assert.AreEqual("The Issuer field is required.", issuerErrorMessage.ErrorMessage);
            Assert.AreEqual("The Cardnumber field is required.", cardnumberErrorMessage.ErrorMessage);
            Assert.AreEqual("The Cvv field is required.", cvvErrorMessage.ErrorMessage);
            Assert.AreEqual("The ExpiryMonth field is required.", expiryMonthErrorMessage.ErrorMessage);
            Assert.AreEqual("The ExpiryYear field is required.", expiryYearErrorMessage.ErrorMessage);
        }

        // a full list of invlaid formats to be included
        [Test]
        [TestCase("1")]
        [TestCase("1234567890123456")]
        [TestCase("qwertyuiopasdfgj")]
        public void PaymentCardReqVM_ShouldReturnErrorIfCardnumberInvlaid(string cardnumber)
        {
            // Arrange

            PaymentCardReqVM paymentCardReqVM = MockPaymentCardReqVM.Get();
            paymentCardReqVM.Cardnumber = cardnumber;

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentCardReqVM, new ValidationContext(paymentCardReqVM), validationResults, true);

            ValidationResult cardnumberErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.Cardnumber));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual("The Cardnumber field is not a valid card number.", cardnumberErrorMessage.ErrorMessage);
        }

        [Test]
        [TestCase("1", Issuer.Amex, "3 or 4 digits for Amex Cards.")]
        [TestCase("1", Issuer.MasterCard, "3 digits.")]
        [TestCase("1", Issuer.Visa, "3 digits.")]
        [TestCase("12", Issuer.Amex, "3 or 4 digits for Amex Cards.")]
        [TestCase("12", Issuer.MasterCard, "3 digits.")]
        [TestCase("12", Issuer.Visa, "3 digits.")]
        [TestCase("1234", Issuer.MasterCard, "3 digits.")]
        [TestCase("1234", Issuer.Visa, "3 digits.")]
        [TestCase("ABCD", Issuer.Amex, "3 or 4 digits for Amex Cards.")]
        [TestCase("ABC", Issuer.MasterCard, "3 digits.")]
        [TestCase("ABC", Issuer.Visa, "3 digits.")]
        public void PaymentCardReqVM_ShouldReturnErrorIfCvvInvlaid(string cvv, Issuer issuer, string error)
        {
            // Arrange

            PaymentCardReqVM paymentCardReqVM = MockPaymentCardReqVM.Get();
            paymentCardReqVM.Cvv = cvv;
            paymentCardReqVM.Issuer = issuer;

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentCardReqVM, new ValidationContext(paymentCardReqVM), validationResults, true);

            ValidationResult cvvErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.Cvv));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual($"The Cvv field must be {error}", cvvErrorMessage.ErrorMessage);
        }

        [Test]
        [TestCase(2020, 10)]
        [TestCase(2019, 1)]
        public void PaymentCardReqVM_ShouldReturnErrorIfExpiryDatesInvlaid(int year, int month)
        {
            // Arrange

            PaymentCardReqVM paymentCardReqVM = MockPaymentCardReqVM.Get();
            paymentCardReqVM.ExpiryYear = year;
            paymentCardReqVM.ExpiryMonth = month;

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentCardReqVM, new ValidationContext(paymentCardReqVM), validationResults, true);

            ValidationResult expiryYearErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.ExpiryYear));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual($"The ExpiryYear and ExpiryMonth may not be in the past.", expiryYearErrorMessage.ErrorMessage);
        }

        [Test]
        [TestCase(0)]
        [TestCase(13)]
        public void PaymentCardReqVM_ShouldReturnErrorIfExpiryMonthInvlaid(int month)
        {
            // Arrange

            PaymentCardReqVM paymentCardReqVM = MockPaymentCardReqVM.Get();
            paymentCardReqVM.ExpiryMonth = month;

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentCardReqVM, new ValidationContext(paymentCardReqVM), validationResults, true);

            ValidationResult expiryMonthErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentCardReqVM.ExpiryMonth));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(1, validationResults.Count);
            Assert.AreEqual($"The field ExpiryMonth must be between 1 and 12.", expiryMonthErrorMessage.ErrorMessage);
        }
    }
}
