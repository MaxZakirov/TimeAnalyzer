using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeAnalyzer.Core.Exceptions
{
    public class DuplicateNameException : Exception
    {
        public DuplicateNameException(string message)
            : base(message)
        { }
    }
}
