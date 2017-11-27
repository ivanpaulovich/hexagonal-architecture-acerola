using System;

namespace Acerola.Exceptions
{
    public class PINShouldNotBeEmptyException : DomainException
    {
        public PINShouldNotBeEmptyException(string message)
            : base(message)
        { }
    }
}
