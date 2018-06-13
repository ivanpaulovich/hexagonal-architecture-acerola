namespace Acerola.DomainTests
{
    using Xunit;
    using Acerola.Domain.Customers;
    using Acerola.Domain.ValueObjects;
    using Acerola.Domain.Accounts;

    public class CustomerTests
    {
        [Fact]
        public void Customer_Should_Be_Registered_With_1_Account()
        {
            //
            // Arrange
            Customer sut = new Customer(new PIN("08724050601"), new Name("Ivan Paulovich"));
            var account = new Account(sut.GetId());

            //
            // Act
            sut.Register(account.GetId());

            //
            // Assert
            Assert.Single(sut.GetAccounts());
        }
    }
}
