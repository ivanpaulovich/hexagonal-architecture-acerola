namespace Acerola.Application.Commands.Deposit
{
    using Acerola.Domain.Customers.Accounts;
    using System.Threading.Tasks;

    public interface IDepositHandler
    {
        Task<Credit> Handle(DepositCommand command);
    }
}
