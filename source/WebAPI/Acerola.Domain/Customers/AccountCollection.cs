namespace Acerola.Domain.Customers
{
    using System.Collections.Generic;
    using System;
    using System.Collections.ObjectModel;

    public class AccountCollection : Collection<Guid>
    {
        public void Add(IEnumerable<Guid> accounts)
        {
            foreach (var account in accounts)
            {
                Items.Add(account);
            }
        }
    }
}
