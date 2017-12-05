using MyAccountAPI.Producer.Application.Queries;
using MongoDB.Driver;
using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyAccountAPI.Producer.Infrastructure.Queries
{
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
