namespace Acerola.Infrastructure.MongoDataAccess.Queries
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using Acerola.Infrastructure.MongoDataAccess.Entities;

    public class CustomersQueries : ICustomersQueries
    {
        private readonly Context _context;
        private readonly IAccountsQueries _accountsQueries;

        public CustomersQueries(Context context, IAccountsQueries accountsQueries)
        {
            _context = context;
            _accountsQueries = accountsQueries;
        }

        public async Task<CustomerResult> GetCustomer(Guid id)
        {
            Customer customer = await _context
                .Customers
                .Find(e => e.Id == id)
                .SingleOrDefaultAsync();

            if (customer == null)
                throw new CustomerNotFoundException($"The customer {id} does not exists or is not processed yet.");

            List<Guid> accountIds = await _context
                .Accounts
                .Find(e => e.CustomerId == id)
                .Project(p => p.Id)
                .ToListAsync();

            List<AccountResult> accountsResult = new List<AccountResult>();

            foreach (Guid accountId in accountIds)
            {
                AccountResult accountResult = await _accountsQueries.GetAccount(accountId);
                accountsResult.Add(accountResult);
            }

            CustomerResult customerResult = new CustomerResult(customer.Id, customer.SSN, customer.Name, null);

            customerResult = new CustomerResult(
                customerResult.CustomerId,
                customerResult.Personnummer,
                customerResult.Name,
                accountsResult);

            return customerResult;
        }
    }
}
