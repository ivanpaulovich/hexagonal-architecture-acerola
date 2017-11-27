using System;

namespace Acerola.Exceptions
{
    public class InsuficientFundsException : DomainException
    {
        public InsuficientFundsException(string message)
            : base(message)
        { }
    }
}
