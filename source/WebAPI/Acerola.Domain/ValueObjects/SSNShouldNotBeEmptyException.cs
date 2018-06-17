namespace Acerola.Domain.ValueObjects
{
    public class SSNShouldNotBeEmptyException : DomainException
    {
        internal SSNShouldNotBeEmptyException(string message)
            : base(message)
        { }
    }
}
