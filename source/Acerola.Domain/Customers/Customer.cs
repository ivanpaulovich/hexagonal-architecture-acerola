namespace Acerola.Domain.Customers
{
    using System.Collections.Generic;
    using System;
    using Acerola.Domain.ValueObjects;
    using Acerola.Domain.Accounts;

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

            accounts = new List<Account>();
            accounts.Add(account);
        }
    }
}
