namespace Acerola.Application.Boundary
{
    using Acerola.Application.UseCases;
    using Acerola.Domain.Accounts;
    using System.Threading.Tasks;

    public interface IDeposit
    {
        Task<Credit> Handle(DepositMessage message);
    }
}
