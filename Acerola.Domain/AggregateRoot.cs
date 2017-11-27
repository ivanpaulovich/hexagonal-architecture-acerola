using System;
using System.Collections.Generic;

namespace Acerola
{
    public abstract class AggregateRoot : Entity
    {
        private readonly Dictionary<Type, Action<object>> handlers = new Dictionary<Type, Action<object>>();
        private readonly List<DomainEvent> domainEvents = new List<DomainEvent>();
        
        public int Version { get; private set; }

        public AggregateRoot()
        {
            Version = 0;
        }

        protected void Register<T>(Action<T> when)
        {
            handlers.Add(typeof(T), e => when((T)e));
        }

        protected void Raise(DomainEvent domainEvent)
        {
            domainEvents.Add(domainEvent);
            handlers[domainEvent.GetType()](domainEvent);
            Version++;
        }

        public IReadOnlyCollection<DomainEvent> GetEvents()
        {
            return domainEvents;
        }

        public void Apply(DomainEvent domainEvent)
        {
            handlers[domainEvent.GetType()](domainEvent);
            Version++;
        }
    }
}
