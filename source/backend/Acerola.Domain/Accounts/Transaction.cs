namespace Acerola.Domain.Accounts
{
    using Acerola.Domain.ValueObjects;

    public class Transaction : Entity
    {
        public Amount Amount { get; protected set; }

        protected Transaction()
        {

        }
    }
}
