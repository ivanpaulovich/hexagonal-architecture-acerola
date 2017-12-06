using System.Threading.Tasks;

namespace Acerola.Domain.Accounts
{
    public interface IAccountWriteOnlyRepository
    {
        Task Add(Account account);
        Task Update(Account account);
        Task Delete(Account account);
    }
}
