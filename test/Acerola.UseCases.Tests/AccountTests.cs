namespace Acerola.UseCases.Tests
{
    using Xunit;
    using Acerola.Domain.Customers;
    using NSubstitute;
    using System;
    using Acerola.Application.Commands.Register;
    using Acerola.Application.Commands.Deposit;
    using Acerola.Domain.Accounts;
    using Acerola.Application.Repositories;

    public class AccountTests
    {
        public IAccountReadOnlyRepository accountReadOnlyRepository;
        public IAccountWriteOnlyRepository accountWriteOnlyRepository;
        public ICustomerReadOnlyRepository customerReadOnlyRepository;
        public ICustomerWriteOnlyRepository customerWriteOnlyRepository;

        public AccountTests()
        {
            accountReadOnlyRepository = Substitute.For<IAccountReadOnlyRepository>();
            accountWriteOnlyRepository = Substitute.For<IAccountWriteOnlyRepository>();
            customerReadOnlyRepository = Substitute.For<ICustomerReadOnlyRepository>();
            customerWriteOnlyRepository = Substitute.For<ICustomerWriteOnlyRepository>();
        }

        [Theory]
        [InlineData("08724050601", "Ivan Paulovich", 300)]
        [InlineData("08724050601", "Ivan Paulovich Pinheiro Gomes", 100)]
        [InlineData("08724050601", "Ivan Paulovich", 500)]
        [InlineData("08724050601", "Ivan Paulovich", 100)]
        public async void Register_Valid_User_Account(string personnummer, string name, double amount)
        {
            var registerUseCase = new RegisterUseCase(
                customerWriteOnlyRepository,
                accountWriteOnlyRepository
            );

            RegisterResult result = await registerUseCase.Execute(
                personnummer,
                name,
                amount);

            Assert.Equal(personnummer, result.Customer.Personnummer);
            Assert.Equal(name, result.Customer.Name);
            Assert.True(Guid.Empty != result.Customer.CustomerId);
            Assert.True(Guid.Empty != result.Account.AccountId);
        }


        [Theory]
        [InlineData("c725315a-1de6-4bf7-aecf-3af8f0083681", 100)]
        public async void Deposit_Valid_Amount(string accountId, double amount)
        {
            var account = new Account(Guid.NewGuid());
            var customer = new Customer("08724050601", "Ivan Paulovich");

            accountReadOnlyRepository
                .Get(Guid.Parse(accountId))
                .Returns(account);

            var depositUseCase = new DepositUseCase(
                accountReadOnlyRepository,
                accountWriteOnlyRepository
            );

            DepositResult result = await depositUseCase.Execute(
                Guid.Parse(accountId),
                amount);

            Assert.Equal(amount, result.Transaction.Amount);
        }

        [Theory]
        [InlineData(100)]
        public void Account_With_Credits_Should_Not_Allow_Close(double amount)
        {
            var account = new Account(Guid.NewGuid());
            account.Deposit(amount);

            Assert.Throws<AccountCannotBeClosedException>(
                () => account.Close());
        }
    }
}
