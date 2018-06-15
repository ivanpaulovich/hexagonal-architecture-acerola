namespace Acerola.Domain.Customers
{
    using System;
    using System.Collections.Generic;
    using Acerola.Domain.ValueObjects;

    public class Customer : IEntity, IAggregateRoot
    {
        public Guid Id { get; }
        public Name Name { get; }
        public PIN PIN { get; }
        public AccountCollection Accounts { get; }

        public Customer(Guid id, Name name, PIN pin, AccountCollection accounts)
        {
            Id = id;
            Name = name;
            PIN = pin;
            Accounts = accounts;
        }

        public Customer(PIN pin, Name name)
        {
            Id = Guid.NewGuid();
            PIN = pin;
            Name = name;
            Accounts = new AccountCollection();
        }

        public void Register(Guid accountId)
        {
            Accounts.Add(accountId);
        }
    }
}
