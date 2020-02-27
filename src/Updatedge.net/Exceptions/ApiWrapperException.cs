using System;
using System.Runtime.Serialization;
using Udatedge.Common.Models;

namespace Updatedge.net.Exceptions
{
    public class ApiWrapperException : Exception
    {
        public RangeRestrictionProblemDetails ExceptionDetails { get; set; }

        public ApiWrapperException()
        {
        }

        public ApiWrapperException(string message) : base(message)
        {
        }

        public ApiWrapperException(RangeRestrictionProblemDetails validationDetails) : base()
        {
            ExceptionDetails = validationDetails;
        }

        public ApiWrapperException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ApiWrapperException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
