using MongoDB.Driver;
using Acerola.Domain.Model.Customers;
using System;
using System.Threading.Tasks;

namespace MyAccountAPI.Producer.Infrastructure.DataAccess.Repositories.Customers
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
