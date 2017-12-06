using Acerola.Domain.Customers;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Acerola.Infrastructure.DataAccess.Repositories.Customers
{
    public class CustomerWriteOnlyRepository : ICustomerWriteOnlyRepository
    {
        private readonly MongoContext mongoContext;
        public CustomerWriteOnlyRepository(MongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        public async Task Add(Customer customer)
        {
            await mongoContext.Customers.InsertOneAsync(customer);
        }

        public async Task Update(Customer customer)
        {
            await mongoContext.Customers.ReplaceOneAsync(e => e.Id == customer.Id, customer);
        }
    }
}
