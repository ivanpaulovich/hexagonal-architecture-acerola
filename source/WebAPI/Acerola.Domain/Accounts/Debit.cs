namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System;

    public class Debit : IEntity, ITransaction
    {
        private Guid _id;
        private Guid _accountId;
        private Amount _amount;

        public Debit(Guid accountId, Amount amount)
        {
            _id = Guid.NewGuid();
            _accountId = accountId;
            _amount = amount;
        }

        public string GetDescription()
        {
            return "Debit";
        }

        public Amount GetAmount()
        {
            return -_amount;
        }

        public Guid GetId()
        {
            return _id;
        }
    }
}
