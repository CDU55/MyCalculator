using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    public class InvalidNumberArrayException : Exception
    {
        public InvalidNumberArrayException(string message) : base(message)
        {

        }
    }
}
