using System.Collections.Generic;

namespace MyCalculator.Parsers
{
    public interface IPostfixCalculator
    {
        int[] CalculateFromPostfix(List<string> postfix);
    }
}