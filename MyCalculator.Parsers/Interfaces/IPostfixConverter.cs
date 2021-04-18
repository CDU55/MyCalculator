using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers
{
    public interface IPostfixConverter
    {
        List<string> ConvertToPostfix(List<string> tokens);
    }
}
