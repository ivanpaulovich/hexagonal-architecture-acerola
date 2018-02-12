namespace Acerola.Application.Commands.Register
{
    using Acerola.Domain.Customers;
    using System.Threading.Tasks;

    public interface IRegisterHandler
    {
        Task<Customer> Handle(RegisterCommand message);
    }
}
