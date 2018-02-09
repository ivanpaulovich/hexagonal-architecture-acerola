namespace Acerola.Application.Boundary
{
    using Acerola.Application.UseCases;
    using Acerola.Domain.Accounts;
    using System.Threading.Tasks;

    public interface IWithdraw
    {
        Task<Debit> Handle(WithdrawMessage message);
    }
}
