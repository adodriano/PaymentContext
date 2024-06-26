﻿using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Shared.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Bruce";
            command.LastName = "Wayne";
            command.Document = "99999999999";
            command.Email = "RZUeh@example.com";

            command.BarCode = "123456789";
            command.BoletoNumber = "123456789";

            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);

            command.Total = 60;
            command.TotalPaid = 60;

            command.Payer = "WAYNE CORP";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "dc@example.com";

            command.Street = "asdasd";
            command.City = "asdasd";
            command.State = "as";
            command.Country = "as";
            command.ZipCode = "12345678";


            handler.Handle(command);

            Assert.AreEqual(false, handler.IsValid);


        }
    }
}
