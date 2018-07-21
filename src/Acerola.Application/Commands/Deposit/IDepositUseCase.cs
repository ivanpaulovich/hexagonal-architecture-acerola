namespace Acerola.Application.Commands.Deposit
{
    using Acerola.Domain.ValueObjects;
    using System;
    using System.Threading.Tasks;

    public interface IDepositUseCase
    {
        Task<DepositResult> Execute(Guid accountId, Amount amount);
    }
}
