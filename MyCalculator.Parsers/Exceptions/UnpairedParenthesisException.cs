using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    public class UnpairedParenthesisException:Exception
    {
        public UnpairedParenthesisException(string message) : base(message)
        {

        }
    }
}
