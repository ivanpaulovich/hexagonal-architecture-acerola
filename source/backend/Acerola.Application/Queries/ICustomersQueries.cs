namespace Acerola.Application.Queries
{
    using Acerola.Application.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICustomersQueries
    {
        Task<CustomerVM> GetCustomer(Guid id);
        Task<IEnumerable<CustomerVM>> GetAll();
    }
}
