using Acerola.Domain.Accounts;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Acerola.Infrastructure.DataAccess.Repositories.Accounts
{
    public class AccountWriteOnlyRepository : IAccountWriteOnlyRepository
    {
        private readonly MongoContext mongoContext;
        public AccountWriteOnlyRepository(MongoContext mongoContext)
        {
            this.mongoContext = mongoContext;
        }

        public async Task Add(Account account)
        {
            await mongoContext.Accounts.InsertOneAsync(account);
        }

        public async Task Delete(Account account)
        {
            await mongoContext.Accounts.DeleteOneAsync(e => e.Id == account.Id);
        }

        public async Task Update(Account account)
        {
            await mongoContext.Accounts.ReplaceOneAsync(e => e.Id == account.Id, account);
        }
    }
}
