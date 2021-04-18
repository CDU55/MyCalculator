using System;
using System.Collections.Generic;
using System.Text;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers
{
    public class TextTokenizer : ITokenizer
    {
        private readonly TokenValidator _validator;

        public TextTokenizer(TokenValidator validator)
        {
            _validator = validator;
        }
        public List<string> Tokenize(string input)
        {
            List<string> output = new List<string>();
            string currentNumber = "";
            foreach (var character in input)
            {
                if(_validator.IsValid(character.ToString()))
                {
                    throw new InvalidTokenException($"{character} is not a valid character");
                }

                if (!_validator.IsNumber(character.ToString()))
                {
                    if (character != ' ')
                    {
                        output.Add(character.ToString());
                    }

                    if (!string.IsNullOrEmpty(currentNumber))
                    {
                        output.Add(currentNumber);
                        currentNumber = "";
                    }
                }
                else
                {
                    currentNumber += character.ToString();
                }
            }

            return output;
        }
    }
}
