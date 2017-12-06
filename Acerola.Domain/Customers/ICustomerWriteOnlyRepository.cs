using System.Threading.Tasks;

namespace Acerola.Domain.Customers
{
    public interface ICustomerWriteOnlyRepository
    {
        Task Add(Customer customer);
        Task Update(Customer customer);
    }
}
