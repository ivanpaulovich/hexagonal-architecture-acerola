namespace Acerola.Domain.Customers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Acerola.Domain.ValueObjects;

    public class Customer : IEntity, IAggregateRoot
    {
        public Guid Id { get; }
        public Name Name { get; }
        public PIN PIN { get; }
        public ReadOnlyCollection<Guid> Accounts
        {
            get
            {
                ReadOnlyCollection<Guid> readOnly = new ReadOnlyCollection<Guid>(_accounts);
                return readOnly;
            }
        }

        private AccountCollection _accounts;

        public Customer(Guid id, Name name, PIN pin, AccountCollection accounts)
        {
            Id = id;
            Name = name;
            PIN = pin;
            _accounts = accounts;
        }

        public Customer(PIN pin, Name name)
        {
            Id = Guid.NewGuid();
            PIN = pin;
            Name = name;
            _accounts = new AccountCollection();
        }

        public void Register(Guid accountId)
        {
            _accounts.Add(accountId);
        }
    }
}
