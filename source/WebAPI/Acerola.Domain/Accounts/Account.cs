namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System;

    public class Account : IEntity, IAggregateRoot
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public TransactionCollection Transactions { get; }

        public Account(Guid customerId)
        {
            Id = Guid.NewGuid();
            Transactions = new TransactionCollection();
            CustomerId = customerId;
        }

        public void Deposit(Amount amount)
        {
            Credit credit = new Credit(Id, amount);
            Transactions.Add(credit);
        }

        public void Withdraw(Amount amount)
        {
            if (Transactions.GetCurrentBalance() < amount)
                throw new InsuficientFundsException($"The account {Id} does not have enough funds to withdraw {amount}.");

            Debit debit = new Debit(Id, amount);
            Transactions.Add(debit);
        }

        public void Close()
        {
            if (Transactions.GetCurrentBalance() > 0)
                throw new AccountCannotBeClosedException($"The account {Id} can not be closed because it has funds.");
        }
    }
}
