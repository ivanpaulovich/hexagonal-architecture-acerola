using MyAccountAPI.Producer.Application.Commands.Customers;
using MediatR;
using System;
using System.Threading.Tasks;
using MyAccountAPI.Domain.ServiceBus;
using MyAccountAPI.Domain.Model.Customers;
using MyAccountAPI.Domain.Model.ValueObjects;
using MyAccountAPI.Domain.Model.Accounts;

namespace MyAccountAPI.Producer.Application.CommandHandlers.Customers
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
