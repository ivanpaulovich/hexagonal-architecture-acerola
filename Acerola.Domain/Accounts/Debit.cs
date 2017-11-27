using Acerola.ValueObjects;
using System;

namespace Acerola.Accounts
{
    public class Debit : Transaction
    {
        private Debit()
        {

        }

        private Debit(Amount amount)
            : base()
        {

        }

        public static Debit Create(Guid customerId, Amount amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));

            Debit debit = new Debit();
            debit.customerId = customerId;
            debit.amount = amount;
            return debit;
        }

        public static Debit Load(Guid id, Guid customerId, Amount amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));

            Debit debit = new Debit();
            debit.Id = id;
            debit.customerId = customerId;
            debit.amount = amount;
            return debit;
        }
    }
}
