using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using MyCalculator.Parsers.CodeContractsUitls;
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
            Contract.Assert(tokens!=null && tokens.Count>0);
            Contract.Ensures(Contract.Result<List<string>>().Count==tokens.Count);
            Contract.EnsuresOnThrow<UnpairedParenthesisException>(!ParserVerifier.CheckUnpairedParenthesis(tokens));
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
                    //Bug here, the conditions were not properly checked ( left associative operators with equal precedence )
                    while (operators.Count != 0 && operators.Peek() != "(" &&
                           ((GetPrecedence(operators.Peek()) > GetPrecedence(tokens[i]))
                            || (operators.Count != 0 &&
                                GetPrecedence(tokens[i]) == GetPrecedence(operators.Peek()) &&
                                IsLeftAssociative(tokens[i]))))
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
                    //Bug here, no initial check for empty stack
                    if (operators.Count == 0)
                    {
                        throw new UnpairedParenthesisException("There is a parenthesis without pair");
                    }

                    while (operators.Peek() != "(")
                    {
                        output.Enqueue(operators.Pop());
                        if (operators.Count == 0)
                        {
                            throw new UnpairedParenthesisException("There is a parenthesis without pair");
                        }
                    }

                    operators.Pop();
                }

                i++;
            }

            if (i == tokens.Count)
            {
                while (operators.Count != 0)
                {
                    //Check for left parenthesis not necessary
                    if (operators.Peek() == ")" || operators.Peek() == "(")
                    {
                        throw new UnpairedParenthesisException("There is a parenthesis without pair");
                    }

                    output.Enqueue(operators.Pop());
                }
            }

            while (output.Count != 0)
            {
                postfix.Add(output.Peek());
                output.Dequeue();
            }

            return postfix;
        }

        public int GetPrecedence(string operatorString)
        {
            if (!_validator.IsOperator(operatorString))
            {
                throw new InvalidOperatorException($"{operatorString} is not an operator");
            }

            switch (operatorString)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                case "#":
                    return 3;
                default:
                    return -1;
            }
        }

        public bool IsLeftAssociative(string operatorString)
        {
            Contract.Assert(operatorString!=null && operatorString.Length==1);
            if (!_validator.IsOperator(operatorString))
            {
                throw new InvalidOperatorException($"{operatorString} is not an operator");
            }

            if (operatorString == "+" || operatorString == "-" || operatorString == "*" || operatorString == "/")
            {
                return true;
            }

            return false;
        }
    }
}