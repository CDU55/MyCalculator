using System;
using System.Collections.Generic;
using System.Text;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers
{
    public class PostfixCalculator : IPostfixCalculator
    {
        private readonly ITokenValidator _validator;
        private readonly ICalculator _calculator;

        public PostfixCalculator(ITokenValidator validator,ICalculator calculator)
        {
            _validator = validator;
            _calculator = calculator;
        }
        public int[] CalculateFromPostfix(List<string> postfix)
        {
            Stack<int[]> result = new Stack<int[]>();
            foreach (var token in postfix)
            {
                
                if (_validator.IsNumber(token))
                {
                    result.Push(token.ConvertToArrayNumber());
                }

                else if (token == "#")
                {
                    if (result.Count < 1)
                    {
                        throw new InvalidTokenException("Invalid expression");
                    }

                    int[] currentToken = result.Pop();
                    
                    if (!_calculator.IsSquareNumber(currentToken))
                    {
                        throw new NotSquareNumberException($"{currentToken.ConvertToString()} is not a square number");
                    }

                    currentToken = _calculator.SquareRoot(currentToken);
                    result.Push(currentToken);
                }
                else if (_validator.IsOperator(token))
                {
                    if (result.Count < 2)
                    {
                        throw new InvalidTokenException("Invalid expression");
                    }
                    int[] firstNumber = result.Pop();
                    int[] secondNumber = result.Pop();
                    int[] operationResult;
                    switch (token[0])
                    {
                        case '+':
                            operationResult = _calculator.AddNumbers(firstNumber, secondNumber);
                            break;
                        case '-':
                            operationResult = _calculator.SubtractNumbers(firstNumber, secondNumber);
                            break;
                        case '*':
                            operationResult = _calculator.MultiplyNumbers(firstNumber, secondNumber);
                            break;
                        case '/':
                            operationResult = _calculator.DivideNumbers(firstNumber, secondNumber);
                            break;
                        case '^':
                            operationResult = _calculator.RaiseToPower(firstNumber, secondNumber);
                            break;
                        default:
                            operationResult = new int[] { -1 };
                            break;
                    }
                    result.Push(operationResult);
                }
            }

            return result.Pop();
        }
    }
}
