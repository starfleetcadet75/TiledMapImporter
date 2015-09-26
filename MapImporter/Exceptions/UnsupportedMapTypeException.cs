using System;
using System.Runtime.Serialization;

namespace MapImporter.Exceptions
{
    [Serializable]
    public class UnsupportedMapTypeException : Exception
    {
        public UnsupportedMapTypeException()
        {
        }

        public UnsupportedMapTypeException(string message) : base(message)
        {
        }

        public UnsupportedMapTypeException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedMapTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
