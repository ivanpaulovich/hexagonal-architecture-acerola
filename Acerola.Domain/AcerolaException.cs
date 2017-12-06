using System;

namespace Acerola.Domain
{
    public class AcerolaException : Exception
    {
        public AcerolaException()
        { }

        public AcerolaException(string message)
            : base(message)
        { }

        public AcerolaException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
