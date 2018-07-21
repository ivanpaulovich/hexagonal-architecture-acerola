namespace Acerola.Infrastructure.InMemoryDataAccess.Queries
{
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using System.Collections.Generic;
    using System.Linq;
    using Acerola.Domain.Accounts;

    public class AccountsQueries : IAccountsQueries
    {
        private readonly Context context;

        public AccountsQueries(Context context)
        {
            this.context = context;
        }

        public async Task<AccountResult> GetAccount(Guid accountId)
        {
            Account data = context
                .Accounts
                .Where(e => e.Id == accountId)
                .SingleOrDefault();

            if (data == null)
                throw new AccountNotFoundException($"The account {accountId} does not exists or is not processed yet.");

            List<ITransaction> credits = data
                .GetTransactions()
                .Where(e => e is Credit)
                .ToList();

            List<ITransaction> debits = data
                .GetTransactions()
                .Where(e => e is Debit)
                .ToList();

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

            return await Task.FromResult<AccountResult>(accountResult);
        }
    }
}
