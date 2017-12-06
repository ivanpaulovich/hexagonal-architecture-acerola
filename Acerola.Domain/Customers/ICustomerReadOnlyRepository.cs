using System;
using System.Threading.Tasks;

namespace Acerola.Domain.Customers
{
    public interface ICustomerReadOnlyRepository
    {
        Task<Customer> Get(Guid id);
    }
}
