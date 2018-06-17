namespace Acerola.Application.Results
{
    using System;

    public sealed class TransactionResult
    {
        public string Description { get; }
        public double Amount { get; }
        public DateTime TransactionDate { get; }

        public TransactionResult(
            string description,
            double amount,
            DateTime transactionDate)
        {
            Description = description;
            Amount = amount;
            TransactionDate = transactionDate;
        }
    }
}
