namespace Acerola.Application.Commands.Withdraw
{
    using Acerola.Domain.ValueObjects;
    using System;
    using System.Threading.Tasks;

    public interface IWithdrawUseCase
    {
        Task<WithdrawResult> Execute(Guid accountId, Amount amount);
    }
}
