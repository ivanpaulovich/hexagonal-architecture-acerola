using Acerola.Customers.Events;
using System.Collections.Generic;
using System;
using Acerola.ValueObjects;
using Acerola.Accounts;

namespace Acerola.Customers
{
    public class Customer : AggregateRoot
    {
        private Name name;
        private PIN pin;
        private List<Account> accounts;

        public Name GetName()
        {
            return name;
        }

        public PIN GetPIN()
        {
            return pin;
        }

        public IReadOnlyCollection<Account> GetAccounts()
        {
            return accounts;
        }

        private Customer()
        {
            Register<RegisteredDomainEvent>(When);            
        }

        public static Customer Create()
        {
            Customer customer = new Customer();
            return customer;
        }

        public static Customer Create(PIN pin, Name name)
        {
            if (pin == null)
                throw new ArgumentNullException(nameof(pin));

            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Customer customer = new Customer();
            customer.pin = pin;
            customer.name = name;
            return customer;
        }

        public void Register(Account account)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            Raise(RegisteredDomainEvent.Create(
                this, this.GetName(), this.GetPIN(), 
                account.Id, account.GetCurrentBalance()));
        }

        protected void When(RegisteredDomainEvent domainEvent)
        {
            if (domainEvent == null)
                throw new ArgumentNullException(nameof(domainEvent));

            Id = domainEvent.AggregateRootId;
            name = domainEvent.Name;
            pin = domainEvent.PIN;

            Account account = Account.Load(domainEvent.AccountId, domainEvent.InitialAmount);

            accounts = new List<Account>();
            accounts.Add(account);
        }
    }
}
