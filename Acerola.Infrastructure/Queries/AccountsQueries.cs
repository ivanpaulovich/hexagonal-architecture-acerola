using MyAccountAPI.Producer.Application.Queries;
using MongoDB.Driver;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyAccountAPI.Producer.Infrastructure.Queries
{
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
            return await Accounts.Find(Builders<ExpandoObject>.Filter.Eq("_id", id)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ExpandoObject>> GetAsync()
        {
            return await Accounts.Find(e => true).ToListAsync();
        }
    }
}
