using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers
{
    public static class Extensions
    {
        public static string ConvertToString(this int[] number)
        {
            string numberString = "";
            foreach (var digit in number)
            {
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
            int[] numberArray = new int[number.Length];
            int arrayDigitIndex = 0;
            for (int digitIndex=number.Length-1;digitIndex>=0;digitIndex--)
            {
                numberArray[arrayDigitIndex++] = number[digitIndex]-'0';
            }
            return numberArray;
        }
    }
}
