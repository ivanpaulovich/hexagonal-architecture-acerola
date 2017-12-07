namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;
    using System;

    public class Credit : Transaction
    {
        private Credit()
        {

        }

        public static Credit Create(Guid customerId, Amount amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));

            Credit credit = new Credit();
            credit.customerId = customerId;
            credit.amount = amount;
            return credit;
        }
    }
}
