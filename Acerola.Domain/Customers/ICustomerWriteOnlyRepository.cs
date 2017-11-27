using System.Threading.Tasks;

namespace Acerola.Customers
{
    public interface ICustomerWriteOnlyRepository
    {
        Task Add(Customer customer);
        Task Update(Customer customer);
    }
}
