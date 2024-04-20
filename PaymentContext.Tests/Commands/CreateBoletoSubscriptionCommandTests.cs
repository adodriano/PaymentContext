namespace PaymentContext.Domain.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "";

            command.Validate();

            Assert.AreEqual(false, command.IsValid);

        }
    }
}
