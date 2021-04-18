using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers
{
    public interface ITokenizer
    {
        List<string> Tokenize(string input);
    }
}
