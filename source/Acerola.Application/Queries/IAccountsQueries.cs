namespace Acerola.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Threading.Tasks;

    public interface IAccountsQueries
    {
        Task<ExpandoObject> GetAsync(Guid id);
        Task<IEnumerable<ExpandoObject>> GetAsync();
    }
}
