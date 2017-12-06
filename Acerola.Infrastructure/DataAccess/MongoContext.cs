using Acerola.Domain;
using Acerola.Domain.Accounts;
using Acerola.Domain.Customers;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Acerola.Infrastructure.DataAccess
{
    public class MongoContext
    {
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase database;

        public MongoContext(string connectionString, string databaseName)
        {
            this.mongoClient = new MongoClient(connectionString);
            this.database = mongoClient.GetDatabase(databaseName);
            Map();
        }

        public void DatabaseReset(string databaseName)
        {
            mongoClient.DropDatabase(databaseName);
        }

        public IMongoCollection<Customer> Customers
        {
            get
            {
                return database.GetCollection<Customer>("Customers");
            }
        }

        public IMongoCollection<Account> Accounts
        {
            get
            {
                return database.GetCollection<Account>("Accounts");
            }
        }

        private void Map()
        {
            BsonClassMap.RegisterClassMap<Entity>(cm =>
            {
                cm.MapIdProperty(c => c.Id);
            });

            BsonClassMap.RegisterClassMap<AggregateRoot>(cm =>
            {
                cm.MapProperty(c => c.Version).SetElementName("_version");
            });

            BsonClassMap.RegisterClassMap<Account>(cm =>
            {
                cm.MapField("currentBalance").SetElementName("currentBalance");
                cm.MapField("transactions").SetElementName("transactions");
                cm.MapField("customerId").SetElementName("customerId");
            });

            BsonClassMap.RegisterClassMap<Transaction>(cm =>
            {
                cm.MapField("amount").SetElementName("amount");
            });

            BsonClassMap.RegisterClassMap<Credit>();

            BsonClassMap.RegisterClassMap<Debit>();

            BsonClassMap.RegisterClassMap<Customer>(cm =>
            {
                cm.MapField("name").SetElementName("name");
                cm.MapField("pin").SetElementName("pin");
            });
        }
    }
}
