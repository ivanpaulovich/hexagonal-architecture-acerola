namespace Acerola.Application.Results
{
    using System;
    using System.Collections.Generic;

    public class AccountResult
    {
        public Guid AccountId { get; }
        public double CurrentBalance { get; }
        public List<TransactionResult> Transactions { get; }

        public AccountResult(Guid accountId, double currentBalance, List<TransactionResult> transactions)
        {
            this.AccountId = accountId;
            this.CurrentBalance = currentBalance;
            this.Transactions = transactions;
        }
    }
}
