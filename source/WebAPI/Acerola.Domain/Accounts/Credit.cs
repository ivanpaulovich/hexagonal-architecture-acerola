namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System;

    public class Credit : IEntity, ITransaction
    {
        private Guid _id;
        private Guid _accountId;
        private Amount _amount;

        public Credit(Guid accountId, Amount amount)
        {
            _id = Guid.NewGuid();
            _accountId = accountId;
            _amount = amount;
        }

        public string GetDescription()
        {
            return "Credit";
        }

        public Amount GetAmount()
        {
            return _amount;
        }

        public Guid GetId()
        {
            return _id;
        }
    }
}
