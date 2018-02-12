namespace Acerola.Application.Results
{
    using System;

    public class TransactionResult
    {
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
