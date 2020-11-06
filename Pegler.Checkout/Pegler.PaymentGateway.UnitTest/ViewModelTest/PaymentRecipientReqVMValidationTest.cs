using NUnit.Framework;
using Pegler.PaymentGateway.ViewModels.Payment.POST;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Pegler.PaymentGateway.UnitTest.ViewModelTest
{
    [TestFixture]
    public class PaymentRecipientReqVMValidationTest
    {
        [Test]
        public void PaymentRecipientReqVM_ShouldReturnErrorIfValuesAreNull()
        {
            // Arrange

            PaymentRecipientReqVM paymentRecipientReqVM = new PaymentRecipientReqVM();

            List<ValidationResult> validationResults = new List<ValidationResult>();

            // Act

            bool isValid = Validator.TryValidateObject(paymentRecipientReqVM, new ValidationContext(paymentRecipientReqVM), validationResults, true);

            ValidationResult nameErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentRecipientReqVM.Name));

            ValidationResult sortCodeErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentRecipientReqVM.SortCode));

            ValidationResult accountNumberErrorMessage = validationResults
                .FirstOrDefault(e => e.MemberNames.FirstOrDefault() == nameof(paymentRecipientReqVM.Accountnumber));

            // Assert

            Assert.IsFalse(isValid);
            Assert.AreEqual(3, validationResults.Count);
            Assert.AreEqual("The Name field is required.", nameErrorMessage.ErrorMessage);
            Assert.AreEqual("The SortCode field is required.", sortCodeErrorMessage.ErrorMessage);
            Assert.AreEqual("The Accountnumber field is required.", accountNumberErrorMessage.ErrorMessage);
        }
    }
}
