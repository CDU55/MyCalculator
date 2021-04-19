using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    public class InvalidNumberSizeException : Exception
    {
        public InvalidNumberSizeException(string message) : base(message)
        {

        }
    }
}