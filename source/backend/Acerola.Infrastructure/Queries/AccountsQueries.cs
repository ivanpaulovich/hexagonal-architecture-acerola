namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using Acerola.Domain;
    using Acerola.Domain.Accounts;
    using Acerola.Domain.Customers;
    using MongoDB.Bson.Serialization;
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
            this.Map();
        }

        private void Map()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Entity)))
                BsonClassMap.RegisterClassMap<Entity>(cm =>
                {
                    cm.AutoMap();
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(AggregateRoot)))
                BsonClassMap.RegisterClassMap<AggregateRoot>(cm =>
                {
                    cm.AutoMap();
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(Account)))
                BsonClassMap.RegisterClassMap<Account>(cm =>
                {
                    cm.AutoMap();
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(Transaction)))
                BsonClassMap.RegisterClassMap<Transaction>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIsRootClass(true);
                    cm.AddKnownType(typeof(Debit));
                    cm.AddKnownType(typeof(Credit));
                });

            if (!BsonClassMap.IsClassMapRegistered(typeof(Credit)))
                BsonClassMap.RegisterClassMap<Credit>();
            if (!BsonClassMap.IsClassMapRegistered(typeof(Debit)))
                BsonClassMap.RegisterClassMap<Debit>();

            if (!BsonClassMap.IsClassMapRegistered(typeof(Customer)))
                BsonClassMap.RegisterClassMap<Customer>(cm =>
                {
                    cm.AutoMap();
                });
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

            var result = await Accounts.Find(Builders<ExpandoObject>.Filter.Eq("CustomerId", customerId)).ToListAsync();
            return result;
        }
    }
}
