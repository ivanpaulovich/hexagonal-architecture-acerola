namespace Acerola.Application.ViewModels
{
    using System;
    using System.Collections.Generic;

    public class AccountVM
    {
        public Guid AccountId { get; set; }
        public Guid CustomerId { get; set; }
        public double CurrentBalance { get; set; }
        public List<TransactionVM> Transactions { get; set; }
    }
}
