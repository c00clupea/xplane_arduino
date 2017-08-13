using System;

namespace ArduinoConnector.Exceptions
{
    public class NotInitializedException : Exception
    {
        public NotInitializedException() { }
        public NotInitializedException(String Message) : base(Message) { }
        public NotInitializedException(String Message, Exception innerException) : base(Message, innerException) { }
    }
}
