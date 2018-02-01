namespace Acerola.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Threading.Tasks;

    public interface ICustomersQueries
    {
        Task<ExpandoObject> GetAsync(Guid id);
        Task<IEnumerable<ExpandoObject>> GetAsync();
    }
}
