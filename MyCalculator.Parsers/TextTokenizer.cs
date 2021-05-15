using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers
{
    public class TextTokenizer : ITokenizer
    {
        private readonly ITokenValidator _validator;

        public TextTokenizer(ITokenValidator validator)
        {
            _validator = validator;
        }
        public List<string> Tokenize(string input)
        {
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(input),"Null parameter to tokenize");
            List<string> output = new List<string>();
            Contract.Ensures(output.Count <= input.Length);
            Contract.Ensures(Contract.ForAll(output,
                (string token) => _validator.IsNumber(token) || _validator.IsOperator(token) ||
                                  _validator.IsParenthesis(token)));
            string currentNumber = "";
            foreach (var character in input)
            {
                Contract.Assert(output.Count<input.Length);
                if(!_validator.IsValid(character.ToString()))
                {
                    throw new InvalidTokenException($"{character} is not a valid character");
                }

                if (!_validator.IsNumber(character.ToString()))
                {
                    if (!string.IsNullOrEmpty(currentNumber))
                    {
                        output.Add(currentNumber);
                        currentNumber = "";
                    }
                    if (character != ' ')
                    {
                        output.Add(character.ToString());
                    }
                }
                else
                {
                    currentNumber += character.ToString();
                }
            }

            //Bug found here, no empty string check before adding
            if (!string.IsNullOrEmpty(currentNumber))
            {
                output.Add(currentNumber);
            }

            return output;
        }
    }
}
