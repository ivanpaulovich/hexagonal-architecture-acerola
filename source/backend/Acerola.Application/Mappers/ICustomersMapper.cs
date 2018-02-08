namespace Acerola.Application.Mappers
{
    using Acerola.Application.DTO;
    using Acerola.Domain.Customers;

    public interface ICustomersMapper
    {
        CustomerData Map(Customer customer);
    }
}
