namespace Acerola.Infrastructure.MongoDataAccess.Queries
{
    using MongoDB.Driver;
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using Acerola.Infrastructure.MongoDataAccess.Entities;
    using System.Collections.Generic;
    using System.Linq;

    public class AccountsQueries : IAccountsQueries
    {
        private readonly Context context;

        public AccountsQueries(Context context)
        {
            this.context = context;
        }

        public async Task<AccountResult> GetAccount(Guid accountId)
        {
            Account data = await context
                .Accounts
                .Find(e => e.Id == accountId)
                .SingleOrDefaultAsync();

            if (data == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists or is not processed yet.");

            List<Credit> credits = await context
                .Credits
                .Find(e => e.Id == accountId)
                .ToListAsync();

            List<Debit> debits = await context
                .Debits
                .Find(e => e.Id == accountId)
                .ToListAsync();

            double credit = credits.Sum(c => c.Amount);
            double debit = debits.Sum(d => d.Amount);

            List<TransactionResult> transactionResults = new List<TransactionResult>();

            foreach (Credit transaction in credits)
            {
                TransactionResult transactionResult = new TransactionResult(
                    transaction.Description, transaction.Amount, transaction.TransactionDate);
                transactionResults.Add(transactionResult);
            }

            foreach (Debit transaction in debits)
            {
                TransactionResult transactionResult = new TransactionResult(
                    transaction.Description, transaction.Amount, transaction.TransactionDate);
                transactionResults.Add(transactionResult);
            }

            List<TransactionResult> orderedTransactions = transactionResults.OrderBy(e => e.TransactionDate).ToList();

            AccountResult accountResult = new AccountResult(
                data.Id,
                credit - debit,
                orderedTransactions);

            return accountResult;
        }
    }
}
