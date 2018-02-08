namespace Acerola.Domain.UnitTests
{
    using System;
    using Xunit;
    using System.Linq;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.Customers;
    using Acerola.Domain.ValueObjects;
    using Acerola.Application.Commands.Accounts;
    using Acerola.Application.Commands.Customers;
    using Acerola.Application.CommandHandlers.Customers;
    using NSubstitute;
    using Acerola.Application.CommandHandlers.Accounts;

    public class ApplicationTests
    {
        public ICustomerReadOnlyRepository customerReadOnlyRepository;
        public ICustomerWriteOnlyRepository customerWriteOnlyRepository;

        public IAccountReadOnlyRepository accountReadOnlyRepository;
        public IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public ApplicationTests()
        {
            customerReadOnlyRepository = Substitute.For<ICustomerReadOnlyRepository>();
            customerWriteOnlyRepository = Substitute.For<ICustomerWriteOnlyRepository>();
            accountReadOnlyRepository = Substitute.For<IAccountReadOnlyRepository>();
            accountWriteOnlyRepository = Substitute.For<IAccountWriteOnlyRepository>();
        }

        [Fact]
        public async void Register_Valid_User_Account()
        {
            RegisterCustomerCommand command = new RegisterCustomerCommand()
            {
                PIN = "08724050601",
                InitialAmount = 300,
                Name = "Ivan Paulovich"
            };

            RegisterCustomerCommandHandler sut = new RegisterCustomerCommandHandler(
                customerWriteOnlyRepository,
                accountWriteOnlyRepository);

            Customer registered = await sut.Handle(command);

            Assert.Equal(command.PIN, registered.PIN.Text);
            Assert.Equal(command.Name, registered.Name.Text);
        }

        [Fact]
        public async void Deposit_Valid_User_Amount()
        {
            DepositCommand command = new DepositCommand()
            {
                AccountId = Guid.NewGuid(),
                Amount = 600
            };

            accountReadOnlyRepository
                .Get(command.AccountId)
                .Returns(new Account());

            DepositCommandHandler sut = new DepositCommandHandler(
                accountReadOnlyRepository,
                accountWriteOnlyRepository);

            Transaction transaction = await sut.Handle(command);

            Assert.Equal(command.Amount, transaction.Amount.Value);
            Assert.Equal("Credit", transaction.Description);
        }
    }
}
