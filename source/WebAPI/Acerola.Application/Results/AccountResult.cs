namespace Acerola.Application.Results
{
    using System;
    using System.Collections.Generic;

    public class AccountResult
    {
        public Guid AccountId { get; set; }
        public Guid CustomerId { get; set; }
        public double CurrentBalance { get; set; }
        public List<TransactionResult> Transactions { get; set; }
    }
}
