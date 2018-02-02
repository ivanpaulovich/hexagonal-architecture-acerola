namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using Acerola.Domain.Accounts;
    using MongoDB.Driver;
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Threading.Tasks;

    public class AccountsQueries : IAccountsQueries
    {
        private readonly IMongoDatabase database;
        public IMongoCollection<ExpandoObject> Accounts
        {
            get
            {
                return database.GetCollection<ExpandoObject>("Accounts");
            }
        }

        public AccountsQueries(string connectionString, string databaseName)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            if (string.IsNullOrWhiteSpace(databaseName))
                throw new ArgumentNullException(nameof(databaseName));

            MongoClient mongoClient = new MongoClient(connectionString);
            this.database = mongoClient.GetDatabase(databaseName);
        }

        public async Task<ExpandoObject> GetAsync(Guid id)
        {
            var result = await Accounts.Find(Builders<ExpandoObject>.Filter.Eq("_id", id)).SingleOrDefaultAsync();

            if (result == null)
                throw new AccountNotFoundException($"The account {id} does not exists or is not processed yet.");

            return result;
        }

        public async Task<IEnumerable<ExpandoObject>> GetAsync(Guid? customerId)
        {
            if (customerId == null)
                return await Accounts.Find(e => true).ToListAsync();

            var result = await Accounts.Find(Builders<ExpandoObject>.Filter.Eq("customerId", customerId)).ToListAsync();
            return result;
        }
    }
}
