using Acerola.Domain.ValueObjects;
using System;

namespace Acerola.Domain.Accounts
{
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

        public static Credit Load(Guid id, Guid customerId, Amount amount)
        {
            if (amount == null)
                throw new ArgumentNullException(nameof(amount));

            Credit credit = new Credit();
            credit.Id = id;
            credit.customerId = customerId;
            credit.amount = amount;
            return credit;
        }
    }
}
