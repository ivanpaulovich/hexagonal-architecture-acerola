using Acerola.Domain.Accounts;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Acerola.Infrastructure.DataAccess.Repositories.Accounts
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
