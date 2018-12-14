using System;

namespace TimeAnalyzer.Core.Exceptions
{
    public class IncorrectInputDateException : Exception
    {
        public IncorrectInputDateException(string message)
            : base(message)
        { }
    }
}
