using System;
using System.Collections.Generic;
using System.Text;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers
{
    public class PostfixConverter : IPostfixConverter
    {
        private readonly ITokenValidator _validator;

        public PostfixConverter(ITokenValidator validator)
        {
            _validator = validator;
        }
        public List<string> ConvertToPostfix(List<string> tokens)
        {
            int i = 0;
            List<string> postfix = new List<string>();
            Queue<string> output = new Queue<string>();
            Stack<string> operators = new Stack<string>();
            while (i < tokens.Count)
            {
                if (_validator.IsNumber(tokens[i]))
                {
                    output.Enqueue(tokens[i]);
                }
                else if (_validator.IsOperator(tokens[i]))
                {
                    while (((operators.Count!=0 && GetPrecedence(operators.Peek()) > GetPrecedence(tokens[i])
                            || (operators.Count!=0 && tokens[i] == operators.Peek() && tokens[i] != "#"))
                           && operators.Peek() != "("))
                    {
                        output.Enqueue(operators.Pop());
                    }
                    operators.Push(tokens[i]);

                }

                else if (tokens[i] == "(")
                {
                    operators.Push(tokens[i]);

                }

                else if (tokens[i] == ")")
                {

                    while (operators.Peek() != "(")
                    {
                        if (operators.Count==0)
                        {
                            throw new UnpairedParenthesisException("There is a parenthesis without pair");
                        }
                        output.Enqueue(operators.Pop());
                    }
                    operators.Pop();
                }
                i++;
            }

            if (i == tokens.Count)
            {
                while (operators.Count!=0)
                {
                    if (operators.Peek() == ")" || operators.Peek() == "(")
                    {
                        throw new UnpairedParenthesisException("There is a parenthesis without pair");
                    }
                    output.Enqueue(operators.Pop());

                }

            }

            while (output.Count!=0)
            {
                postfix.Add(output.Peek());
                output.Dequeue();

            }

            return postfix;
		}

        public int GetPrecedence(string operatorString)
        {
            if (operatorString == "+" || operatorString == "-")
            {
                return 1;
            }
            if(operatorString=="*" || operatorString=="/")
            {
                return 2;
            }

            if (operatorString == "^" || operatorString == "#")
            {
                return 3;
            }

            return -1;
        }
    }
}
