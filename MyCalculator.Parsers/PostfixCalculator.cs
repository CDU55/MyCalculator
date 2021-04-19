﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers
{
    public class PostfixCalculator : IPostfixCalculator
    {
        private readonly ITokenValidator _validator;
        private readonly ICalculator _calculator;
        private readonly ITokenizer _tokenizer;
        private readonly IPostfixConverter _converter;

        public PostfixCalculator(ITokenValidator validator,ICalculator calculator,ITokenizer tokenizer,IPostfixConverter converter)
        {
            _validator = validator;
            _calculator = calculator;
            _tokenizer = tokenizer;
            _converter = converter;
        }

        public int[] Calculate(string expression,out List<string> steps)
        {
            var tokens = _tokenizer.Tokenize(expression);
            var postfix = _converter.ConvertToPostfix(tokens);
            var result = CalculateFromPostfix(postfix,out steps);
            return result;
        }
        public int[] CalculateFromPostfix(List<string> postfix,out List<string> steps,bool convertSteps=true)
        {
            Stack<int[]> result = new Stack<int[]>();
            steps = new List<string>();
            for (int tokenIndex=0;tokenIndex<postfix.Count;tokenIndex++)
            {
                var token = postfix[tokenIndex];
                
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
                    
                    currentToken = _calculator.SquareRoot(currentToken);
                    if (currentToken.Length == 1 && currentToken[0] == -1)
                    {
                        throw new NotSquareNumberException($"{currentToken.ConvertToString()} is not a square number");
                    }
                    result.Push(currentToken);
                }
                else if (_validator.IsOperator(token))
                {
                    if (result.Count < 2)
                    {
                        throw new InvalidTokenException("Invalid expression");
                    }
                    int[] secondNumber = result.Pop();
                    int[] firstNumber = result.Pop();
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

                if (_validator.IsOperator(token) && tokenIndex<postfix.Count-1)
                {
                    var postfixToSolve = postfix.GetRange(tokenIndex + 1,postfix.Count- tokenIndex - 1);
                    postfixToSolve.InsertRange(0,result.Select(r=>r.ConvertToString()));
                    var currentStep = "";
                    if (convertSteps)
                    {
                        currentStep= FromPostfixToInfix(postfixToSolve);
                    }
                    else
                    {
                        currentStep = string.Join(',',postfixToSolve);
                    }
                    steps.Add(currentStep);
                }
            }

            return result.Pop();
        }
        public string FromPostfixToInfix(List<string> str)
        {
            var tokenValidator = new TokenValidator();
            var stack = new Stack<string>(str.Count);

            for (int j = 0; j < str.Count; j++)
            {
                if (!tokenValidator.IsOperator(str[j]))
                {
                    stack.Push(str[j]);
                }
                else if (str[j] == "#")
                {
                    string operator1 = stack.Pop();
                    stack.Push("("+ str[j] + operator1 + ")");
                }
                else
                {
                    string operator1 = stack.Pop();
                    string operator2 = stack.Pop();
                    stack.Push("(" + operator2 + str[j] + operator1 + ")");
                }
            }
            return stack.Pop();
        }
    }
}
