using System;
using System.Collections.Generic;
using System.Text;

namespace Acerola.Application.DTO
{
    public class TransactionData
    {
        public string Description { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
