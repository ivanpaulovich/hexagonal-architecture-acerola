namespace Acerola.Application.Commands.Close
{
    using System;
    public class CloseResult
    {
        public Guid AccountId { get; private set; }
        public CloseResult()
        {

        }

        public CloseResult(Guid accountId)
        {
            AccountId = accountId;
        }
    }
}
