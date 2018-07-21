namespace Acerola.Application.Queries
{
    using Acerola.Application.Results;
    using System;
    using System.Threading.Tasks;

    public interface IAccountsQueries
    {
        Task<AccountResult> GetAccount(Guid accountId);
    }
}
