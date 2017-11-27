using Acerola.ValueObjects;
using System;

namespace Acerola.Accounts
{
    public class Transaction : Entity
    {
        protected Guid customerId;
        protected Amount amount;

        public Guid GetCustomerId()
        {
            return customerId;
        }

        public Amount GetAmount()
        {
            return amount;
        }

        protected Transaction()
        {

        }
    }
}
