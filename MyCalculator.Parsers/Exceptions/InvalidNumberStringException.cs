using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    public class InvalidNumberStringException : Exception
    {
        public InvalidNumberStringException(string message):base(message)
        {

        }
    }
}
