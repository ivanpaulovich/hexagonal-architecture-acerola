namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using Acerola.Infrastructure.DataAccess;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Acerola.Application;
    using Acerola.Domain.Customers.Accounts;
    using Acerola.Domain.Customers;

    public class AccountsQueries : IAccountsQueries
    {
        private readonly AccountBalanceContext mongoDB;
        private readonly IResultConverter mapper;

        public AccountsQueries(AccountBalanceContext mongoDB, IResultConverter mapper)
        {
            this.mongoDB = mongoDB;
            this.mapper = mapper;
        }

        public async Task<AccountResult> GetAccount(Guid accountId)
        {
            Customer customer = await mongoDB.Customers
                .Find(Builders<Customer>.Filter.ElemMatch(x => x.Accounts, e => e.Id == accountId))
                .SingleOrDefaultAsync();

            Account data = null;

            foreach (var item in customer.Accounts)
            {
                if (item.Id == accountId)
                    data = item;
            }

            if (data == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists or is not processed yet.");

            AccountResult accountVM = this.mapper.Map<AccountResult>(data);

            return accountVM;
        }
    }
}
