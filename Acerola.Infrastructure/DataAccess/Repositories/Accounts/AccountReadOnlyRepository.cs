using MongoDB.Driver;
using Acerola.Domain.Model.Accounts;
using System;
using System.Threading.Tasks;

namespace MyAccountAPI.Producer.Infrastructure.DataAccess.Repositories.Accounts
{
    public class AccountReadOnlyRepository : IAccountReadOnlyRepository
    {
        private readonly MongoContext _mongoContext;

        public AccountReadOnlyRepository(MongoContext mongoContext)
        {
            _mongoContext = mongoContext;
        }

        public async Task<Account> Get(Guid id)
        {
            return await _mongoContext.Accounts.Find(e => e.Id == id).SingleOrDefaultAsync();
        }
    }
}
