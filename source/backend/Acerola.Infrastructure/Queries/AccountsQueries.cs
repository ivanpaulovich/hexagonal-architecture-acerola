namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using Acerola.Application.ViewModels;
    using Acerola.Domain.Accounts;
    using Acerola.Infrastructure.DataAccess;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AccountsQueries : IAccountsQueries
    {
        private readonly AccountBalanceContext mongoDB;

        public AccountsQueries(AccountBalanceContext mongoDB)
        {
            this.mongoDB = mongoDB;
        }

        public async Task<AccountVM> GetAccount(Guid id)
        {
            Account data = await this.mongoDB.Accounts
                .Find(Builders<Account>.Filter.Eq("_id", id))
                .SingleOrDefaultAsync();

            if (data == null)
                throw new AccountNotFoundException($"The account {id} does not exists or is not processed yet.");

            List<TransactionVM> transactions = new List<TransactionVM>();

            foreach (Transaction transaction in data.Transactions)
            {
                TransactionVM transactionVM = new TransactionVM()
                {
                    Amount = transaction.Amount.Value,
                    Description = transaction.Description,
                    TransactionDate = transaction.TransactionDate
                };

                transactions.Add(transactionVM);
            }

            AccountVM accountVM = new AccountVM()
            {
                AccountId = data.Id,
                CustomerId = data.CustomerId,
                CurrentBalance = data.CurrentBalance.Value,
                Transactions = transactions
            };

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
                List<TransactionVM> transactions = new List<TransactionVM>();

                foreach (Transaction transaction in item.Transactions)
                {
                    TransactionVM transactionVM = new TransactionVM()
                    {
                        Amount = transaction.Amount.Value,
                        Description = transaction.Description,
                        TransactionDate = transaction.TransactionDate
                    };

                    transactions.Add(transactionVM);
                }

                AccountVM accountVM = new AccountVM()
                {
                    AccountId = item.Id,
                    CustomerId = item.CustomerId,
                    CurrentBalance = item.CurrentBalance.Value,
                    Transactions = transactions
                };

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
                List<TransactionVM> transactions = new List<TransactionVM>();

                foreach (Transaction transaction in item.Transactions)
                {
                    TransactionVM transactionVM = new TransactionVM()
                    {
                        Amount = transaction.Amount.Value,
                        Description = transaction.Description,
                        TransactionDate = transaction.TransactionDate
                    };

                    transactions.Add(transactionVM);
                }

                AccountVM accountVM = new AccountVM()
                {
                    AccountId = item.Id,
                    CustomerId = item.CustomerId,
                    CurrentBalance = item.CurrentBalance.Value,
                    Transactions = transactions
                };

                result.Add(accountVM);
            }

            return result;
        }
    }
}
