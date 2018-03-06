namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;
    using Acerola.Infrastructure.DataAccess;
    using Acerola.Domain.Customers;
    using Acerola.Application;
    using Acerola.Application.Results;
    using System.Collections.Generic;
    using Acerola.Domain.Accounts;

    public class CustomersQueries : ICustomersQueries
    {
        private readonly AccountBalanceContext mongoDB;
        private readonly IResultConverter resultConverter;

        public CustomersQueries(AccountBalanceContext mongoDB, IResultConverter resultConverter)
        {
            this.mongoDB = mongoDB;
            this.resultConverter = resultConverter;
        }

        public async Task<CustomerResult> GetCustomer(Guid id)
        {
            Customer data = await mongoDB
                .Customers
                .Find(e => e.Id == id)
                .SingleOrDefaultAsync();

            if (data == null)
                throw new CustomerNotFoundException($"The customer {id} does not exists or is not processed yet.");

            //
            // TODO: The following queries could be simplified
            //

            List<AccountResult> accounts = new List<AccountResult>();

            foreach (var accountId in data.Accounts.Items)
            {
                Account account = await mongoDB
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
