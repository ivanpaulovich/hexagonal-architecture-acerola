namespace Acerola
{
    public class DomainException : AcerolaException
    {
        public string BusinessMessage { get; set; }

        public DomainException(string businessMessage)
        {
            BusinessMessage = businessMessage;
        }
    }
}
