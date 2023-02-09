using System;
using System.Runtime.Serialization;

namespace Updatedge.net.Exceptions
{
    public class ThrottledApiRequestException : Exception
    {
        public ThrottledApiRequestException()
        {
        }

        public ThrottledApiRequestException(string message) : base(message)
        {
        }

        public ThrottledApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ThrottledApiRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
