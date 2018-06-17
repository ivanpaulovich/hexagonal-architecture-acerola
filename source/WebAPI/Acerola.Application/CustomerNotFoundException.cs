namespace Acerola.Application
{
    public class CustomerNotFoundException : ApplicationException
    {
        internal CustomerNotFoundException(string message)
            : base(message)
        { }
    }
}
