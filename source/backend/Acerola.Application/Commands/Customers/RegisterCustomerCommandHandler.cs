namespace Acerola.Application.CommandHandlers.Customers
{
    using Acerola.Application.Commands.Customers;
    using MediatR;
    using System;
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;
    using Acerola.Domain.ValueObjects;
    using Acerola.Domain.Accounts;
    using Acerola.Application.Commands.Accounts;

    public class RegisterCustomerCommandHandler : IAsyncRequestHandler<RegisterCustomerCommand, Customer>
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;
        private readonly IMediator mediator;

        public RegisterCustomerCommandHandler(
            ICustomerWriteOnlyRepository customerWriteOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository,
            IMediator mediator)
        {
            if (customerWriteOnlyRepository == null)
                throw new ArgumentNullException(nameof(customerWriteOnlyRepository));

            if (accountWriteOnlyRepository == null)
                throw new ArgumentNullException(nameof(accountWriteOnlyRepository));

            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator));

            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
            this.mediator = mediator;
        }

        public async Task<Customer> Handle(RegisterCustomerCommand command)
        {
            Customer customer = Customer.Create(
                PIN.Create(command.PIN),
                Name.Create(command.Name));

            Account account = Account.Create(customer);
            customer.Register(account);

            await customerWriteOnlyRepository.Add(customer);

            Transaction transaction = Credit.Create(
                customer.Id, Amount.Create(command.InitialAmount));
            account.Deposit(transaction);

            await accountWriteOnlyRepository.Add(account);

            return customer;
        }
    }
}
