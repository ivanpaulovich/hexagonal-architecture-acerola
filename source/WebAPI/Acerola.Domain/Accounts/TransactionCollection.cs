namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class TransactionCollection : Collection<ITransaction>
    {
        public void Add(IEnumerable<ITransaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                Items.Add(transaction);
            }
        }

        public Amount GetCurrentBalance()
        {
            Amount totalAmount = 0;
            
            foreach (var item in Items)
            {
                if (item is Debit)
                    totalAmount = totalAmount - item.Amount;

                if (item is Credit)
                    totalAmount = totalAmount + item.Amount;
            }

            return totalAmount;
        }
    }
}
