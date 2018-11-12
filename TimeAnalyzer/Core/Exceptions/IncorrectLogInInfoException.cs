using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeAnalyzer.Core.Exceptions
{
    public class IncorrectLogInInfoException : Exception
    {
        public IncorrectLogInInfoException(string message)
            : base(message)
        { }
    }
}
