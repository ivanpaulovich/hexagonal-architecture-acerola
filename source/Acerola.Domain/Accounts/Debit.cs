namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System;

    public class Debit : Transaction
    {
        private Debit()
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
    }
}
