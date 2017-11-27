using System;

namespace Acerola
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
