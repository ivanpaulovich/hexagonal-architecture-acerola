namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using Acerola.Application.ViewModels;
    using Acerola.Domain.Accounts;
    using Acerola.Infrastructure.AutoMapper;
    using Acerola.Infrastructure.DataAccess;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AccountsQueries : IAccountsQueries
    {
        private readonly AccountBalanceContext mongoDB;
        private readonly DomainConverter domainConverter;

        public AccountsQueries(AccountBalanceContext mongoDB, DomainConverter domainConverter)
        {
            this.mongoDB = mongoDB;
            this.domainConverter = domainConverter;
        }

        public async Task<AccountVM> GetAccount(Guid id)
        {
            Account data = await this.mongoDB.Accounts
                .Find(Builders<Account>.Filter.Eq("_id", id))
                .SingleOrDefaultAsync();

            if (data == null)
                throw new AccountNotFoundException($"The account {id} does not exists or is not processed yet.");

            AccountVM accountVM = this.domainConverter.Map(data);

            return accountVM;
        }

        public async Task<IEnumerable<AccountVM>> GetAll()
        {
            IEnumerable<Account> data = await this.mongoDB.Accounts
                .Find(e => true)
                .ToListAsync();

            List<AccountVM> result = new List<AccountVM>();

            foreach (Account item in data)
            {
                AccountVM accountVM = this.domainConverter.Map(item);

                result.Add(accountVM);
            }

            return result;
        }

        public async Task<IEnumerable<AccountVM>> Get(Guid customerId)
        {
            IEnumerable<Account> data = await this.mongoDB.Accounts
                .Find(Builders<Account>
                .Filter.Eq("CustomerId", customerId))
                .ToListAsync();

            List<AccountVM> result = new List<AccountVM>();

            foreach (Account item in data)
            {
                AccountVM accountVM = this.domainConverter.Map(item);

                result.Add(accountVM);
            }

            return result;
        }
    }
}
