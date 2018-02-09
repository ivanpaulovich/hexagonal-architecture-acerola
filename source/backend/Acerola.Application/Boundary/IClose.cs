namespace Acerola.Application.Boundary
{
    using Acerola.Application.UseCases;
    using Acerola.Domain.Customers;
    using System.Threading.Tasks;

    public interface IClose
    {
        Task Handle(CloseMessage message);
    }
}
