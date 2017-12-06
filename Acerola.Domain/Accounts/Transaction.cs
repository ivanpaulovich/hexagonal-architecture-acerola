using Acerola.Domain.ValueObjects;
using System;

namespace Acerola.Domain.Accounts
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
