using System;
using System.Runtime.Serialization;

namespace Updatedge.net.Exceptions
{
    public class ApiWrapperException : Exception
    {
        public ApiWrapperException()
        {
        }

        public ApiWrapperException(string message) : base(message)
        {
        }

        public ApiWrapperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiWrapperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
