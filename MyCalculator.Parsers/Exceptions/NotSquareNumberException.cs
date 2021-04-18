using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    public class NotSquareNumberException:Exception
    {
        public NotSquareNumberException(string message):base(message)
        {

        }
    }
}
