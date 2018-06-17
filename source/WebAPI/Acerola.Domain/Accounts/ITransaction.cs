namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System;

    public interface ITransaction
    {
        Amount Amount { get; }
        string Description { get; }
        DateTime TransactionDate { get; }
    }
}
