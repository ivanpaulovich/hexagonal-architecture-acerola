using Acerola.Domain.Accounts;
using Acerola.Domain.Customers;
using Acerola.Domain.Customers.Events;
using MediatR;
using System;

namespace Acerola.Application.EventHandlers.Customers
{
    public class RegisteredEventHandler : IRequestHandler<RegisteredDomainEvent>
    {
        private readonly ICustomerWriteOnlyRepository customerWriteOnlyRepository;
        private readonly IAccountWriteOnlyRepository accountWriteOnlyRepository;

        public RegisteredEventHandler(
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

        public void Handle(RegisteredDomainEvent domainEvent)
        {
            Customer customer = Customer.Create();
            customer.Apply(domainEvent);
            customerWriteOnlyRepository.Add(customer).Wait();

            Account account = Account.Create(domainEvent.AggregateRootId);
            account.Apply(domainEvent);
            accountWriteOnlyRepository.Add(account).Wait();
        }
    }
}
