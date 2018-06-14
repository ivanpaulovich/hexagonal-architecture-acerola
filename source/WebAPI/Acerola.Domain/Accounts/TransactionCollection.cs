namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class TransactionCollection : Collection<ITransaction>
    {
        public TransactionCollection()
        {

        }

        public TransactionCollection(IEnumerable<ITransaction> list)
        {
            foreach (var item in list)
            {
                Items.Add(item);
            }
        }

        internal Amount GetCurrentBalance()
        {
            Amount totalAmount = 0;
            
            foreach (var item in Items)
            {
                totalAmount += item.GetAmount();
            }

            return totalAmount;
        }
    }
}
