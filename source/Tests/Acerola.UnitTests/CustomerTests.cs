namespace Acerola.Domain.UnitTests
{
    using Xunit;
    using Acerola.Domain.Customers;
    using NSubstitute;
    using Acerola.Domain.Customers.Accounts;
    using Acerola.Domain.ValueObjects;

    public class CustomerTests
    {
        [Fact]
        public void Customer_Should_Be_Registered_With_1_Account()
        {
            //
            // Arrange
            Customer sut = new Customer(new PIN("08724050601"), new Name("Ivan Paulovich"));
            Account account = Substitute.For<Account>();

            //
            // Act
            sut.Register(account);

            //
            // Assert
            Assert.Equal(1, sut.Accounts.Count);
        }
    }
}
