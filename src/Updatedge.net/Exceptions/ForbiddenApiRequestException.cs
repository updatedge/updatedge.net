using System;
using System.Runtime.Serialization;

namespace Updatedge.net.Exceptions
{
    public class ForbiddenApiRequestException : Exception
    {
        public ForbiddenApiRequestException()
        {
        }

        public ForbiddenApiRequestException(string message) : base(message)
        {
        }

        public ForbiddenApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ForbiddenApiRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
