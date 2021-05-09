using System;
using System.Collections.Generic;
using System.Text;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers
{
    public static class Extensions
    {
        public static string ConvertToString(this int[] number)
        {
            string numberString = "";
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
            return numberArray;
        }
    }
}
