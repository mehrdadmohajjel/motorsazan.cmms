using System;

namespace ClientApp.Models.Base
{
    public class AlertException: Exception
    {
        public AlertException() { }

        public AlertException(string message) : base(message) { }

        public AlertException(string message, Exception inner) : base(message, inner) { }
    }
}