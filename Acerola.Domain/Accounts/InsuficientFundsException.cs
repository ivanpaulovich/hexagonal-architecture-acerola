namespace Acerola.Accounts
{
    public class InsuficientFundsException : DomainException
    {
        public InsuficientFundsException(string message)
            : base(message)
        { }
    }
}
