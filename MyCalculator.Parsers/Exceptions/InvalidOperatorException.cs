using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    public class InvalidOperatorException : Exception
    {
        public InvalidOperatorException(string message) : base(message)
        {

        }
    }
}
