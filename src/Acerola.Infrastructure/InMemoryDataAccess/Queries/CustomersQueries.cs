namespace Acerola.Infrastructure.InMemoryDataAccess.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using Acerola.Domain.Customers;

    public class CustomersQueries : ICustomersQueries
    {
        private readonly Context _context;
        private readonly IAccountsQueries _accountsQueries;

        public CustomersQueries(Context context, IAccountsQueries accountsQueries)
        {
            _context = context;
            _accountsQueries = accountsQueries;
        }

        public async Task<CustomerResult> GetCustomer(Guid customerId)
        {
            Customer customer = _context
                .Customers
                .Where(e => e.Id == customerId)
                .SingleOrDefault();

            if (customer == null)
                throw new CustomerNotFoundException($"The customer {customerId} does not exists or is not processed yet.");

            List<AccountResult> accountsResult = new List<AccountResult>();

            foreach (Guid accountId in customer.Accounts)
            {
                AccountResult accountResult = await _accountsQueries.GetAccount(accountId);
                accountsResult.Add(accountResult);
            }

            CustomerResult customerResult = new CustomerResult(
                customer.Id, customer.Name, customer.SSN,
                accountsResult);

            return await Task.FromResult<CustomerResult>(customerResult);
        }
    }
}
