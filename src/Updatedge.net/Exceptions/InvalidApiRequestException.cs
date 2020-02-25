﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Updatedge.net.Exceptions
{
    public class InvalidApiRequestException : Exception
    {
        public InvalidApiRequestException()
        {
        }

        public InvalidApiRequestException(string message) : base(message)
        {
        }

        public InvalidApiRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidApiRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
