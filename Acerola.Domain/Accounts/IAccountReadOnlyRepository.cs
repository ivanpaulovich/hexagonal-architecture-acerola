using System;
using System.Threading.Tasks;

namespace Acerola.Accounts
{
    public interface IAccountReadOnlyRepository
    {
        Task<Account> Get(Guid id);
    }
}
