namespace Acerola.Application.Queries
{
    using Acerola.Application.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAccountsQueries
    {
        Task<AccountVM> GetAccount(Guid id);
        Task<IEnumerable<AccountVM>> GetAll();
        Task<IEnumerable<AccountVM>> Get(Guid customerId);
    }
}
