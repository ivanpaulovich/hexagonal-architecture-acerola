namespace Acerola.Domain.Customers
{
    using System;
    using Acerola.Domain.ValueObjects;

    public class Customer : IEntity, IAggregateRoot
    {
        private Guid _id;
        private Name _name;
        private PIN _pin;
        private AccountCollection _accounts;

        public Customer(PIN pin, Name name)
        {
            _pin = pin;
            _name = name;
        }

        public virtual void Register(Guid accountId)
        {
            _accounts = new AccountCollection();
            _accounts.Add(accountId);
        }

        public Guid GetId()
        {
            return _id;
        }

        public AccountCollection GetAccounts()
        {
            return _accounts;
        }
    }
}
