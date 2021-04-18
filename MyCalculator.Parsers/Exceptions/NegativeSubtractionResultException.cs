using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    class NegativeSubtractionResultException : Exception
    {
        public NegativeSubtractionResultException(string message) : base(message)
        {

        }
    }
}
