using MediatR;
using MyAccountAPI.Domain.Model.Accounts;
using MyAccountAPI.Domain.Model.Customers;
using MyAccountAPI.Domain.Model.Customers.Events;
using System;

namespace MyAccountAPI.Consumer.Application.DomainEventHandlers.Customers
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
