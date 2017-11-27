using System;

namespace Acerola.Exceptions
{
    public class NameShouldNotBeEmptyException : DomainException
    {
        public NameShouldNotBeEmptyException(string message)
            : base(message)
        { }
    }
}
