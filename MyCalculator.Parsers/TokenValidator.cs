using System.Collections.Generic;
using System.Numerics;

namespace MyCalculator.Parsers
{
    public class TokenValidator
    {
        private readonly List<string> _operators = new List<string>() {"+", "-", "*", "/","^","#"};
        private readonly List<string> _specialCharacters = new List<string>() {"(", ")"};
        public bool IsNumber(string input)
        {
            if (input[0] == '0' && input.Length > 1)
            {
                return false;
            }
            foreach (var digit in input)
            {
                if (digit < '0' || digit > '9')
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsOperator(string input)
        {
            return _operators.IndexOf(input) > -1;
        }

        public bool IsParenthesis(string input)
        {
            return input == "(" || input == ")";
        }

        public bool IsLeftParenthesis(string input)
        {
            return input == "(";
        }

        public bool IsRightParenthesis(string input)
        {
            return input == ")";
        }

        public bool IsSpace(string input)
        {
            return input == " ";
        }

        public bool IsValid(string input)
        {
            return IsNumber(input) || IsOperator(input) || IsParenthesis(input) || IsSpace(input);
        }
    }
}
