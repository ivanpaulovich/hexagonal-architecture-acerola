namespace Acerola.DomainTests
{
    using Xunit;
    using Acerola.Domain.Customers;
    using Acerola.Domain.ValueObjects;
    using Acerola.Domain.Accounts;
    using System.Collections.Generic;
    using System;

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

        [Fact]
        public void Customer_Should_Be_Loaded()
        {
            AccountCollection accounts = new AccountCollection();
            accounts.Add(Guid.NewGuid());

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("id", Guid.NewGuid());
            input.Add("name", new Name("Sammy Fredriksson"));
            input.Add("pin", new PIN("741214-3054"));
            input.Add("accounts", accounts);

            Customer customer = Customer.Import(input);
            Dictionary<string, object> output = customer.Export();

            foreach (var value in input)
            {
                Assert.Equal(value.Value, output[value.Key]);
            }
        }
    }
}
