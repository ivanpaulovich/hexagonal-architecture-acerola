using Acerola.Application.Commands.Customers;
using MediatR;
using System;
using System.Threading.Tasks;
using Acerola.Domain.ServiceBus;
using Acerola.Domain.Customers;
using Acerola.Domain.ValueObjects;
using Acerola.Domain.Accounts;

namespace Acerola.Application.CommandHandlers.Customers
{
    public class RegisterCustomerCommandHandler : IAsyncRequestHandler<RegisterCustomerCommand, Customer>
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public RegisterCustomerCommandHandler(
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

        public async Task<Customer> Handle(RegisterCustomerCommand command)
        {
            Customer customer = Customer.Create(
                PIN.Create(command.PIN),
                Name.Create(command.Name));

            Account account = Account.Create(
                customer.Id,
                Amount.Create(command.InitialAmount));

            customer.Register(account);

            customerWriteOnlyRepository.Add(customer).Wait();
            accountWriteOnlyRepository.Add(account).Wait();

            return customer;
        }
    }
}
