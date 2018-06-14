namespace Acerola.Domain
{
    using System;

    public class DomainException : Exception
    {
        private string _businessMessage;
        public string GetBusinessMessage()
        {
            return _businessMessage;
        }

        internal DomainException(string businessMessage)
        {
            _businessMessage = businessMessage;
        }
    }
}
