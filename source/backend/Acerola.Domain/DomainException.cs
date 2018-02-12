namespace Acerola.Domain
{
    public class DomainException : AcerolaException
    {
        public string BusinessMessage { get; private set; }

        public DomainException(string businessMessage)
        {
            BusinessMessage = businessMessage;
        }
    }
}
