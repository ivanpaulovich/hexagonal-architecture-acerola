namespace Acerola.Domain.Customers
{
    using System;
    using System.Collections.Generic;
    using Acerola.Domain.ValueObjects;

    public class Customer : IEntity, IAggregateRoot
    {
        private Guid _id;
        private Name _name;
        private PIN _pin;
        private AccountCollection _accounts;

        private Customer()
        {
        }

        public Customer(PIN pin, Name name)
        {
            _id = Guid.NewGuid();
            _pin = pin;
            _name = name;
            _accounts = new AccountCollection();
        }

        public void Register(Guid accountId)
        {
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

        public static Customer Import(Dictionary<string, object> values)
        {
            Customer customer = new Customer();

            customer._id = (Guid)values["id"];
            customer._name = (Name)values["name"];
            customer._pin = (PIN)values["pin"];
            customer._accounts = (AccountCollection)values["accounts"];

            return customer;
        }

        public Dictionary<string, object> Export()
        {
            Dictionary<string, object> values = new Dictionary<string, object>();
            values.Add("id", _id);
            values.Add("name", _name);
            values.Add("pin", _pin);
            values.Add("accounts", _accounts);

            return values;
        }
    }
}
