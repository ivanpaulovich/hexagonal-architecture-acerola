namespace Acerola.Application.UseCases
{
    using MediatR;
    using System;
    using System.Threading.Tasks;
    using Acerola.Domain.Customers;
    using Acerola.Domain.ValueObjects;
    using Acerola.Domain.Accounts;

    public class Register : IAsyncRequestHandler<RegisterCommand, Customer>
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public Register(
            ICustomerWriteOnlyRepository customerWriteOnlyRepository,
            IAccountWriteOnlyRepository accountWriteOnlyRepository)
        {
            if (customerWriteOnlyRepository == null)
                throw new ArgumentNullException(nameof(customerWriteOnlyRepository));

            if (accountWriteOnlyRepository == null)
                throw new ArgumentNullException(nameof(accountWriteOnlyRepository));

            this.customerWriteOnlyRepository = customerWriteOnlyRepository;
            this.accountWriteOnlyRepository = accountWriteOnlyRepository;
        }

        public async Task<Customer> Handle(RegisterCommand command)
        {
            Customer customer = Customer.Create(
                PIN.Create(command.PIN),
                Name.Create(command.Name));

            Account account = Account.Create(customer);
            customer.Register(account);

            await customerWriteOnlyRepository.Add(customer);

            Credit credit = Credit.Create(Amount.Create(command.InitialAmount));
            account.Deposit(credit);

            await accountWriteOnlyRepository.Add(account);

            return customer;
        }
    }
}
