using Acerola.Domain.ValueObjects;
using System;

namespace Acerola.Domain.Customers.Events
{
    public class RegisteredDomainEvent : DomainEvent
    {
        public Name Name { get; private set; }
        public PIN PIN { get; private set; }
        public Guid AccountId { get; private set; }
        public Amount InitialAmount { get; private set; }

        public RegisteredDomainEvent(
            Guid aggregateRootId, 
            int version, 
            DateTime createdDate, 
            Header header, 
            Name name, 
            PIN pin, 
            Guid accountId, 
            Amount initialAmount)
            : base(aggregateRootId, version, createdDate, header)
        {
            this.Name = name;
            this.PIN = pin;
            this.AccountId = accountId;
            this.InitialAmount = initialAmount;
        }

        public static RegisteredDomainEvent Create(
            AggregateRoot aggregateRoot,
            Name name, 
            PIN pin, 
            Guid accountId, 
            Amount initialAmount)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException(nameof(aggregateRoot));

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (pin == null)
                throw new ArgumentNullException(nameof(pin));

            if (accountId == Guid.Empty)
                throw new ArgumentNullException(nameof(accountId));

            if (initialAmount == null)
                throw new ArgumentNullException(nameof(initialAmount));

            RegisteredDomainEvent domainEvent = new RegisteredDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null, 
                name, pin, accountId, initialAmount);

            return domainEvent;
        }
    }
}
