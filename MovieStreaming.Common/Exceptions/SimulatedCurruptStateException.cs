using System;
using System.Runtime.Serialization;

namespace MovieStreaming.Common.Exceptions
{
    public class SimulatedCurruptStateException : Exception
    {
        public SimulatedCurruptStateException()
        {
        }

        public SimulatedCurruptStateException(string message) : base(message)
        {
        }

        public SimulatedCurruptStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SimulatedCurruptStateException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}