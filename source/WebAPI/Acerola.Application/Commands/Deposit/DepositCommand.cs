namespace Acerola.Application.Commands.Deposit
{
    using System;

    public sealed class DepositCommand
    {
        public Guid AccountId { get; }
        public Double Amount { get; }

        public DepositCommand(
            Guid accountId,
            double amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
