using System;

namespace Acerola.Domain.Accounts.Events
{
    public class ClosedDomainEvent : DomainEvent
    {
        public ClosedDomainEvent(Guid aggregateRootId, int version, 
            DateTime createdDate, Header header)
            : base(aggregateRootId, version, createdDate, header)
        {
        }

        public static ClosedDomainEvent Create(AggregateRoot aggregateRoot)
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException(nameof(aggregateRoot));

            ClosedDomainEvent domainEvent = new ClosedDomainEvent(
                aggregateRoot.Id, aggregateRoot.Version, DateTime.UtcNow, null);

            return domainEvent;
        }
    }
}
