namespace Acerola.Application.Commands.Close
{
    using Acerola.Domain.Accounts;
    using System;

    public sealed class CloseResult
    {
        public Guid AccountId { get; }

        public CloseResult(Account account)
        {
            AccountId = account.Id;
        }
    }
}
