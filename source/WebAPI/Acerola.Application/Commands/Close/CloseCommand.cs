namespace Acerola.Application.Commands.Close
{
    using System;

    public sealed class CloseCommand
    {
        public Guid AccountId { get; }

        public CloseCommand(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
