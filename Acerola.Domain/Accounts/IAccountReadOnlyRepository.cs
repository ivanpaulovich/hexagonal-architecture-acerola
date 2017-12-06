using System;
using System.Threading.Tasks;

namespace Acerola.Domain.Accounts
{
    public interface IAccountReadOnlyRepository
    {
        Task<Account> Get(Guid id);
    }
}
