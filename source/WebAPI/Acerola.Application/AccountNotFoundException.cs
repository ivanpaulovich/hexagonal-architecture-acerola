namespace Acerola.Application
{
    public class AccountNotFoundException : ApplicationException
    {
        internal AccountNotFoundException(string message)
            : base(message)
        { }
    }
}
