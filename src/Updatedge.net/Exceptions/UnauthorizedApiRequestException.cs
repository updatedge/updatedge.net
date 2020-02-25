using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
