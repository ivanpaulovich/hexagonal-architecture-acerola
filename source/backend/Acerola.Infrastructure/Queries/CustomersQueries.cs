namespace Acerola.Infrastructure.Queries
{
    using Acerola.Application.Queries;
    using MongoDB.Driver;
    using System;
    using System.Dynamic;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization;
    using Acerola.Domain.Customers;
    using Acerola.Domain.Accounts;
    using Acerola.Domain;

    public class CustomersQueries : ICustomersQueries
    {
        private readonly IMongoDatabase database;
        public IMongoCollection<ExpandoObject> Customers
        {
            get
            {
                return database.GetCollection<ExpandoObject>("Customers");
            }
        }

        public CustomersQueries(string connectionString, string databaseName)
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
                    cm.UnmapProperty(p => p.Accounts);
                });
        }

        public async Task<ExpandoObject> GetAsync(Guid id)
        {
            return await Customers.Find(Builders<ExpandoObject>.Filter.Eq("_id", id)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ExpandoObject>> GetAsync()
        {
            return await Customers.Find(e => true).ToListAsync();
        }
    }
}
