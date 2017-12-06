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
        private readonly IPublisher bus;

        public RegisterCustomerCommandHandler(IPublisher bus)
        {
            if (bus == null)
                throw new ArgumentNullException(nameof(bus));

            this.bus = bus;
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

            var domainEvents = customer.GetEvents();
            await bus.Publish(domainEvents, command.Header);

            return customer;
        }
    }
}
