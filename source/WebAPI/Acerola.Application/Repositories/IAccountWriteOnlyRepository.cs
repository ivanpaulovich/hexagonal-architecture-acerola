namespace Acerola.Application.Repositories
{
    using Acerola.Domain.Accounts;
    using System.Threading.Tasks;

    public interface IAccountWriteOnlyRepository
    {
        Task Add(Account account, Credit credit);
        Task Update(Account account, ITransaction transaction);
        Task Delete(Account account);
    }
}
