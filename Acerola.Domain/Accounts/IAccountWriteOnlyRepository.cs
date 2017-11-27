using System.Threading.Tasks;

namespace Acerola.Accounts
{
    public interface IAccountWriteOnlyRepository
    {
        Task Add(Account account);
        Task Update(Account account);
        Task Delete(Account account);
    }
}
