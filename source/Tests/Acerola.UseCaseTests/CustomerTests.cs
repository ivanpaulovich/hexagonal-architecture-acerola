namespace Acerola.Domain.UnitTests
{
    using Xunit;
    using Acerola.Domain.Customers;
    using NSubstitute;
    using Acerola.Application;
    using Acerola.Infrastructure.Mappings;
    using System;
    using Acerola.Application.Commands.Register;

    public class CustomerTests
    {
        public ICustomerReadOnlyRepository customerReadOnlyRepository;
        public ICustomerWriteOnlyRepository customerWriteOnlyRepository;

        public IResultConverter converter;

        public CustomerTests()
        {
            customerReadOnlyRepository = Substitute.For<ICustomerReadOnlyRepository>();
            customerWriteOnlyRepository = Substitute.For<ICustomerWriteOnlyRepository>();
            converter = new ResultConverter();
        }

        [Theory]
        [InlineData("08724050601", "Ivan Paulovich", 300)]
        [InlineData("08724050601", "Ivan Paulovich Pinheiro Gomes", 100)]
        [InlineData("444", "Ivan Paulovich", 500)]
        [InlineData("08724050", "Ivan Paulovich", 300)]
        public async void Register_Valid_User_Account(string personnummer, string name, double amount)
        {

            var registerUseCase = new RegisterService(
                customerWriteOnlyRepository,
                converter
            );

            var request = new RegisterCommand(
                personnummer,
                name,
                amount
            );

            RegisterResult result = await registerUseCase.Process(request);

            Assert.Equal(request.PIN, result.Customer.Personnummer);
            Assert.Equal(request.Name, result.Customer.Name);
            Assert.True(result.Customer.CustomerId != Guid.Empty);
            Assert.True(result.Account.AccountId != Guid.Empty);
        }
    }
}
