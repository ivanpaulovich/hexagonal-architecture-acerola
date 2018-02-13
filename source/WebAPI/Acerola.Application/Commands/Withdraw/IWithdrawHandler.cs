namespace Acerola.Application.Commands.Withdraw
{
    using Acerola.Domain.Customers.Accounts;
    using System.Threading.Tasks;

    public interface IWithdrawHandler
    {
        Task<WithdrawResult> Handle(WithdrawCommand message);
    }
}
