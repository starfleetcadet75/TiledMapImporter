using System;
using System.Runtime.Serialization;

namespace MapImporter
{
    [Serializable]
    internal class UnsupportedOrientationException : Exception
    {
        public UnsupportedOrientationException()
        {
        }

        public UnsupportedOrientationException(string message) : base(message)
        {
            message = "The Orientation of a given Map file is not 'Orthogonal'. This version of the Importer library does not support any other types.";
        }

        public UnsupportedOrientationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsupportedOrientationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}