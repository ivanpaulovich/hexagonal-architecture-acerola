namespace Acerola.Infrastructure.MongoDataAccess.Queries
{
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Acerola.Application;
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.Customers;

    public class CustomersQueries : ICustomersQueries
    {
        private readonly Context context;
        private readonly IResultConverter resultConverter;

        public CustomersQueries(Context context, IResultConverter resultConverter)
        {
            this.context = context;
            this.resultConverter = resultConverter;
        }

        public async Task<CustomerResult> GetCustomer(Guid id)
        {
            Customer data = await context
                .Customers
                .Find(e => e.Id == id)
                .SingleOrDefaultAsync();

            if (data == null)
                throw new CustomerNotFoundException($"The customer {id} does not exists or is not processed yet.");

            //
            // TODO: The following queries could be simplified
            //

            List<AccountResult> accounts = new List<AccountResult>();

            foreach (var accountId in data.Accounts)
            {
                Account account = await context
                    .Accounts
                    .Find(e => e.Id == accountId)
                    .SingleOrDefaultAsync();

                if (account == null)
                    throw new CustomerNotFoundException($"The account {accountId} does not exists or is not processed yet.");

                //
                // TODO: The "Accout closed state" is not propagating to the Customer Aggregate
                //

                if (account != null)
                {
                    AccountResult accountOutput = resultConverter.Map<AccountResult>(account);
                    accounts.Add(accountOutput);
                }
            }

            CustomerResult customerResult = resultConverter.Map<CustomerResult>(data);

            customerResult = new CustomerResult(
                customerResult.CustomerId,
                customerResult.Personnummer,
                customerResult.Name,
                accounts);

            return customerResult;
        }
    }
}
