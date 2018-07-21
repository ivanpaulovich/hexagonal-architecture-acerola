namespace Acerola.Infrastructure.DapperDataAccess.Queries
{
    using Dapper;
    using Acerola.Domain.Accounts;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Acerola.Application.Queries;
    using Acerola.Application.Results;

    public class AccountsQueries : IAccountsQueries
    {
        private readonly string _connectionString;

        public AccountsQueries(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<AccountResult> GetAccount(Guid accountId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string accountSQL = @"SELECT * FROM Account WHERE Id = @accountId";
                Entities.Account account = await db
                    .QueryFirstOrDefaultAsync<Entities.Account>(accountSQL, new { accountId });

                if (account == null)
                    return null;

                string credits =
                    @"SELECT * FROM [Credit]
                      WHERE AccountId = @accountId";

                List<ITransaction> transactionsList = new List<ITransaction>();

                using (var reader = db.ExecuteReader(credits, new { accountId }))
                {
                    var parser = reader.GetRowParser<Credit>();

                    while (reader.Read())
                    {
                        ITransaction transaction = parser(reader);
                        transactionsList.Add(transaction);
                    }
                }

                string debits =
                    @"SELECT * FROM [Debit]
                      WHERE AccountId = @accountId";

                using (var reader = db.ExecuteReader(debits, new { accountId }))
                {
                    var parser = reader.GetRowParser<Debit>();

                    while (reader.Read())
                    {
                        ITransaction transaction = parser(reader);
                        transactionsList.Add(transaction);
                    }
                }

                TransactionCollection transactionCollection = new TransactionCollection();

                foreach (var item in transactionsList.OrderBy(e => e.TransactionDate))
                {
                    transactionCollection.Add(item);
                }

                Account result = Account.Load(account.Id, account.CustomerId, transactionCollection);
                AccountResult accountResult = new AccountResult(result);
                return accountResult;
            }
        }
    }
}
