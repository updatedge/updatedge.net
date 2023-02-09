using System;
using System.Runtime.Serialization;

namespace Updatedge.net.Exceptions
{
    public class UnauthorizedApiRequestException : Exception
    {
        public UnauthorizedApiRequestException()
        {
        }

        public UnauthorizedApiRequestException(string message) : base(message)
        {
        }

        public UnauthorizedApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedApiRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
