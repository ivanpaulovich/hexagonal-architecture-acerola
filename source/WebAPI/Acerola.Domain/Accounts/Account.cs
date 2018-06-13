namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System;

    public class Account : IEntity, IAggregateRoot
    {
        private Guid _id;
        private Guid _customerId;
        private TransactionCollection _transactions;

        public Guid GetId()
        {
            return _id;
        }

        public Account(Guid customerId)
        {
            _id = Guid.NewGuid();
            _transactions = new TransactionCollection();
            _customerId = customerId;
        }

        public void Deposit(Amount amount)
        {
            Credit credit = new Credit(_id, amount);
            _transactions.Add(credit);
        }

        public void Withdraw(Amount amount)
        {
            if (_transactions.GetCurrentBalance() < amount)
                throw new InsuficientFundsException($"The account {_id} does not have enough funds to withdraw {amount}.");

            Debit debit = new Debit(_id, amount);
            _transactions.Add(debit);
        }

        public void Close()
        {
            if (_transactions.GetCurrentBalance() > 0)
                throw new AccountCannotBeClosedException($"The account {_id} can not be closed because it has funds.");
        }

        public Amount GetCurrentBalance()
        {
            return _transactions.GetCurrentBalance();
        }

    }
}
