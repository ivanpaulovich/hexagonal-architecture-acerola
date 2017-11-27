using System;
using System.Threading.Tasks;

namespace Acerola.Customers
{
    public interface ICustomerReadOnlyRepository
    {
        Task<Customer> Get(Guid id);
    }
}
