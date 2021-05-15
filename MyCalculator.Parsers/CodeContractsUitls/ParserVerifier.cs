using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.CodeContractsUitls
{
    public class ParserVerifier
    {
        public static bool CheckUnpairedParenthesis(List<string> tokens)
        {
            int parenthesisIndex = 0;
            foreach (var token in tokens)
            {
                if (token == "(")
                {
                    parenthesisIndex++;
                }
                else if(token==")")
                {
                    parenthesisIndex--;
                    if (parenthesisIndex < 0)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
