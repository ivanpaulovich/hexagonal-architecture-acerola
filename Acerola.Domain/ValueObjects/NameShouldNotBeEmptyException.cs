namespace Acerola.ValueObjects
{
    public class NameShouldNotBeEmptyException : DomainException
    {
        public NameShouldNotBeEmptyException(string message)
            : base(message)
        { }
    }
}
