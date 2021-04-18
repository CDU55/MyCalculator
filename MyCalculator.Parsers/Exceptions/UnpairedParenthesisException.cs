using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Exceptions
{
    class UnpairedParenthesisException:Exception
    {
        public UnpairedParenthesisException(string message) : base(message)
        {

        }
    }
}
