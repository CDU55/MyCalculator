using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers
{
    public static class Extensions
    {
        public static string ConvertToString(this int[] number)
        {
            Contract.Assert(number!=null && number.Length>0);
            string numberString = "";
            Contract.Ensures(Contract.ForAll(0, numberString.Length, index => numberString[index] == (number[numberString.Length - index - 1] - '0')));
            Contract.Ensures((numberString.Length == number.Length) || (numberString.Length < number.Length) && number[numberString.Length] == -1);
            foreach (var digit in number)
            {
                if (digit > 9)
                {
                    throw new InvalidNumberArrayException("Invalid array, element greater than 9");
                }

                if (digit < -1)
                {
                    throw new InvalidNumberArrayException("Invalid array, element lower than 9");
                }
                if (digit > -1)
                {
                    numberString = digit + numberString;
                }
                else
                {
                    break;
                }
            }

            return numberString;
        }

        public static int[] ConvertToArrayNumber(this string number)
        {
            Contract.Assert(!string.IsNullOrEmpty(number));

            var numberArray = new int[number.Length];
            var arrayDigitIndex = 0;
            for (var digitIndex=number.Length-1;digitIndex>=0;digitIndex--)
            {
                if (number[digitIndex] < '0' || number[digitIndex] > '9')
                {
                    throw new InvalidNumberStringException($"{number[digitIndex]} is not a valid digit");
                }
                numberArray[arrayDigitIndex++] = number[digitIndex]-'0';
            }

            Contract.Ensures(Contract.ForAll(0, number.Length, index => number[index] == (numberArray[number.Length - index - 1] - '0')));
            Contract.Ensures((numberArray.Length == number.Length));
            return numberArray;
        }
    }
}
