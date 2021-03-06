using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        // default value
        private static int maxArraySize = 1000000000;

        public PostfixCalculator(ITokenValidator validator, ICalculator calculator, ITokenizer tokenizer,
            IPostfixConverter converter)
        {
            _validator = validator;
            _calculator = calculator;
            _tokenizer = tokenizer;
            _converter = converter;
        }

        public int[] Calculate(string expression, out List<string> steps, int maxArray)
        {
            var tokens = _tokenizer.Tokenize(expression);
            var postfix = _converter.ConvertToPostfix(tokens);
            if (maxArray <= 0)
            {
                throw new InvalidNumberSizeException("Invalid number size.");
            }
            maxArraySize = maxArray;
            var result = CalculateFromPostfix(postfix, out steps);
            return result;
        }

        public static void SetMaxArraySize(String maxArray)
        {
            try
            {
                var size= Convert.ToInt32(maxArray);
                if (size <= 0)
                {
                    throw new InvalidNumberSizeException("Invalid number size.");
                }
                maxArraySize = size;
            }
            catch (Exception)
            {
                throw new InvalidNumberSizeException("Invalid number size.");
            }
        }

        public static int GetMaxArraySize()
        {
            return maxArraySize;
        }

        public int[] CalculateFromPostfix(List<string> postfix, out List<string> steps, bool convertSteps = true)
        {
            Contract.Assert(postfix!=null && postfix.Count>0);
            Contract.Ensures((!convertSteps && steps !=null && steps.Count==0) 
                             || (convertSteps && steps != null));
            Stack<int[]> result = new Stack<int[]>();
            steps = new List<string>();

            for (int tokenIndex = 0; tokenIndex < postfix.Count; tokenIndex++)
            {
                Contract.Assert(result.Count > -1);
                var token = postfix[tokenIndex];

                if (_validator.IsNumber(token))
                {
                    int[] temp = token.ConvertToArrayNumber();
                    if (_calculator.ValidArraySize(temp, maxArraySize))
                    {
                        result.Push(temp);
                    }
                    else
                    {
                        throw new InvalidNumberSizeException("Invalid number size.");
                    }
                }

                else if (token == "#")
                {
                    if (result.Count < 1)
                    {
                        throw new InvalidExpressionException("Invalid expression");
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
                        throw new InvalidExpressionException("Invalid expression");
                    }
                    Contract.Assert(result.Count>0);
                    int[] secondNumber = result.Pop();
                    Contract.Assert(result.Count > 0);
                    int[] firstNumber = result.Pop();
                    int[] operationResult;
                    switch (token[0])
                    {
                        case '+':
                            operationResult = _calculator.AddNumbers(firstNumber, secondNumber);
                            if (!_calculator.ValidArraySize(operationResult, maxArraySize))
                            {
                                throw new InvalidNumberSizeException("Invalid number size resulted from sum.");
                            }

                            break;
                        case '-':
                            if (_calculator.Smaller(firstNumber, secondNumber))
                            {
                                throw new NegativeSubtractionResultException("Negative result from subtraction.");
                            }

                            operationResult = _calculator.SubtractNumbers(firstNumber, secondNumber);
                            break;
                        case '*':
                            operationResult = _calculator.MultiplyNumbers(firstNumber, secondNumber);
                            if (!_calculator.ValidArraySize(operationResult, maxArraySize))
                            {
                                throw new InvalidNumberSizeException(
                                    "Invalid number size resulted from multiplication.");
                            }

                            break;
                        case '/':
                            if (_calculator.IsZero(secondNumber) || _calculator.IsMultipleZero(secondNumber))
                            {
                                throw new DivideByZeroException("Tried to divide a number by zero");
                            }

                            operationResult = _calculator.DivideNumbers(firstNumber, secondNumber);
                            break;
                        default:
                            operationResult = _calculator.RaiseToPower(firstNumber, secondNumber);
                            if (!_calculator.ValidArraySize(operationResult, maxArraySize))
                            {
                                throw new InvalidNumberSizeException(
                                    "Invalid number size resulted from raise to power.");
                            }
                            break;
                    }

                    result.Push(operationResult);
                }

                if (_validator.IsOperator(token) && tokenIndex < postfix.Count - 1 && convertSteps)
                {
                    var postfixToSolve = postfix.GetRange(tokenIndex + 1, postfix.Count - tokenIndex - 1);
                    postfixToSolve.InsertRange(0, result.Select(r => r.ConvertToString()));
                    var currentStep = "";
                    currentStep = FromPostfixToInfix(postfixToSolve);
                    steps.Add(currentStep);
                }
            }

            return result.Pop();
        }

        public string FromPostfixToInfix(List<string> postfix)
        {
            Contract.Assert(postfix.Count != 0);
            var stack = new Stack<string>(postfix.Count);
            var tokenValidator = new TokenValidator();
            try
            {
                foreach (var token in postfix)
                {
                    if (!tokenValidator.IsOperator(token))
                    {
                        stack.Push(token);
                    }
                    else if (token == "#")
                    {
                        Contract.Assert(stack.Count>0);
                        string operator1 = stack.Pop();
                        stack.Push("(" + token + operator1 + ")");
                    }
                    else
                    {
                        Contract.Assert(stack.Count > 0);
                        string operator1 = stack.Pop();
                        Contract.Assert(stack.Count > 0);
                        string operator2 = stack.Pop();
                        stack.Push("(" + operator2 + token + operator1 + ")");
                    }
                }

                if (stack.Count != 1)
                {
                    throw new InvalidExpressionException("Invalid postfix notation");
                }

                return stack.Pop();
            }
            catch (InvalidExpressionException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new InvalidExpressionException($"Postfix notation invalid : {e.Message}");
            }
        }
    }
}