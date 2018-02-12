namespace Acerola.Application.Commands.Withdraw
{
    using Acerola.Domain.Customers.Accounts;
    using System.Threading.Tasks;

    public interface IWithdrawHandler
    {
        Task<Debit> Handle(WithdrawCommand message);
    }
}
