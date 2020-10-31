using System;
using System.Runtime.Serialization;

namespace dotNet5781_01_4484_2389
{
    [Serializable]
    internal class SomeException : Exception
    {
        public SomeException()
        {
        }

        public SomeException(string message) : base(message)
        {
        }

        public SomeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SomeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}