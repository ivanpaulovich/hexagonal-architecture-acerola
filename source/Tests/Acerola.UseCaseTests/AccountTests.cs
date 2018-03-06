namespace Acerola.UseCaseTests
{
    using Xunit;
    using Acerola.Domain.Customers;
    using NSubstitute;
    using Acerola.Application;
    using Acerola.Infrastructure.Mappings;
    using System;
    using Acerola.Domain.ValueObjects;
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

        public IResultConverter converter;

        public AccountTests()
        {
            accountReadOnlyRepository = Substitute.For<IAccountReadOnlyRepository>();
            accountWriteOnlyRepository = Substitute.For<IAccountWriteOnlyRepository>();
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
                accountWriteOnlyRepository,
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

            accountReadOnlyRepository
                .Get(Guid.Parse(accountId))
                .Returns(account);

            var depositUseCase = new DepositService(
                accountReadOnlyRepository,
                accountWriteOnlyRepository,
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
