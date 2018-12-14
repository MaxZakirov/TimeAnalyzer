using System;

namespace TimeAnalyzer.Core.Exceptions
{
    public class IncorrectLogInInfoException : Exception
    {
        public IncorrectLogInInfoException(string message)
            : base(message)
        { }
    }
}
