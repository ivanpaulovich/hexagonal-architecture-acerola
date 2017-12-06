using Acerola.Domain.Customers;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Acerola.Infrastructure.DataAccess.Repositories.Customers
{
    public class CustomerReadOnlyRepository : ICustomerReadOnlyRepository
    {
        private readonly MongoContext _mongoContext;

        public CustomerReadOnlyRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<Customer> Get(Guid id)
        {
            return await _mongoContext.Customers.Find(e => e.Id == id).SingleOrDefaultAsync();
        }
    }
}
