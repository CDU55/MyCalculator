using System.Collections.Generic;

namespace MyCalculator.Parsers
{
    public interface IPostfixCalculator
    {
        int[] CalculateFromPostfix(List<string> postfix,out List<string> steps,bool convertSteps=true);
        public string FromPostfixToInfix(List<string> postfix);
        public int[] Calculate(string expression, out List<string> steps, int maxArray);

    }
}