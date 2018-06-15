namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System;

    public class Debit : IEntity, ITransaction
    {
        public Guid Id { get; }
        public Guid AccountId { get; }
        public Amount Amount { get; }
        public string Description
        {
            get { return "Debit"; }
        }

        public Debit(Guid id, Guid accountId, Amount amount)
        {
            Id = id;
            AccountId = accountId;
            Amount = amount;
        }

        public Debit(Guid accountId, Amount amount)
        {
            Id = Guid.NewGuid();
            AccountId = accountId;
            Amount = amount;
        }
    }
}
