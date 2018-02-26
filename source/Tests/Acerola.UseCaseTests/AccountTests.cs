namespace Acerola.UseCaseTests
{
    using Xunit;
    using Acerola.Domain.Customers;
    using NSubstitute;
    using Acerola.Application;
    using Acerola.Infrastructure.Mappings;
    using System;
    using Acerola.Domain.ValueObjects;
    using Acerola.Domain.Customers.Accounts;
    using System.Collections.Generic;
    using Acerola.Application.Commands.Register;
    using Acerola.Application.Commands.Deposit;
    using Acerola.Application.Commands.Withdraw;

    public class AccountTests
    {
        public ICustomerReadOnlyRepository customerReadOnlyRepository;
        public ICustomerWriteOnlyRepository customerWriteOnlyRepository;

        public IResultConverter converter;

        public AccountTests()
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
            Assert.True(Guid.Empty != result.Customer.CustomerId);
            Assert.True(Guid.Empty != result.Account.AccountId);
        }


        [Theory]
        [InlineData("c725315a-1de6-4bf7-aecf-3af8f0083681", 100)]
        public async void Deposit_Valid_Amount(string accountId, double amount)
        {
            var account = Substitute.For<Account>();
            var customer = Substitute.For<Customer>();
            customer.FindAccount(Arg.Any<Guid>())
                .Returns(account);

            customerReadOnlyRepository
                .GetByAccount(Guid.Parse(accountId))
                .Returns(customer);

            var depositUseCase = new DepositService(
                customerReadOnlyRepository,
                customerWriteOnlyRepository,
                converter
            );

            var request = new DepositCommand(
                Guid.Parse(accountId),
                amount
            );

            DepositResult result = await depositUseCase.Process(request);

            Assert.Equal(request.Amount, result.Transaction.Amount);
        }

        [Theory]
        [InlineData(100)]
        public void Account_With_Credits_Should_Not_Allow_Close(double amount)
        {
            var account = new Account();
            account.Deposit(new Credit(new Amount(amount)));

            Assert.Throws<AccountCannotBeClosedException>(
                () => account.Close());
        }
    }
}
