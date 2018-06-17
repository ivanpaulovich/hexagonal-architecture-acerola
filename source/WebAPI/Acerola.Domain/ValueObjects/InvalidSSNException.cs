namespace Acerola.Domain.ValueObjects
{
    public class InvalidSSNException : DomainException
    {
        internal InvalidSSNException(string message)
            : base(message)
        { }
    }
}
