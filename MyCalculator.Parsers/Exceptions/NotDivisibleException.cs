using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    public class NotDivisibleException : Exception
    {
        public NotDivisibleException(string message) : base(message)
        {

        }
    }
}
