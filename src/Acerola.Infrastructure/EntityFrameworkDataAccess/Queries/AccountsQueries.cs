namespace Acerola.Infrastructure.EntityFrameworkDataAccess.Queries
{
    using System;
    using System.Threading.Tasks;
    using Acerola.Application.Queries;
    using Acerola.Application.Results;
    using System.Collections.Generic;
    using System.Linq;
    using Acerola.Domain.Accounts;
    using Microsoft.EntityFrameworkCore;

    public class AccountsQueries : IAccountsQueries
    {
        private readonly Context _context;

        public AccountsQueries(Context context)
        {
            _context = context;
        }

        public async Task<AccountResult> GetAccount(Guid accountId)
        {
            Entities.Account account = await _context
                .Accounts
                .FindAsync(accountId);

            List<Entities.Credit> credits = await _context
                .Credits
                .Where(e => e.AccountId == accountId)
                .ToListAsync();

            List<Entities.Debit> debits = await _context
                .Debits
                .Where(e => e.AccountId == accountId)
                .ToListAsync();

            List<ITransaction> transactions = new List<ITransaction>();

            foreach (Entities.Credit transactionData in credits)
            {
                Credit transaction = Credit.Load(
                    transactionData.Id,
                    transactionData.AccountId,
                    transactionData.Amount,
                    transactionData.TransactionDate);

                transactions.Add(transaction);
            }

            foreach (Entities.Debit transactionData in debits)
            {
                Debit transaction = Debit.Load(
                    transactionData.Id,
                    transactionData.AccountId,
                    transactionData.Amount,
                    transactionData.TransactionDate);

                transactions.Add(transaction);
            }

            var orderedTransactions = transactions.OrderBy(o => o.TransactionDate).ToList();

            TransactionCollection transactionCollection = new TransactionCollection();
            transactionCollection.Add(orderedTransactions);

            Account result = Account.Load(
                account.Id,
                account.CustomerId,
                transactionCollection);

            AccountResult re = new AccountResult(result);
            return re;
        }
    }
}
