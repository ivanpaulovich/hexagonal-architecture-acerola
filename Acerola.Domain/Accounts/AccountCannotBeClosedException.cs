using System;

namespace Acerola.Exceptions
{
    public class AccountCannotBeClosedException : DomainException
    {
        public AccountCannotBeClosedException(string message)
            : base(message)
        { }
    }
}
