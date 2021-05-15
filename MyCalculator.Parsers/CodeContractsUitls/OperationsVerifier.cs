using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace MyCalculator.Parsers.CodeContractsUitls
{
    public static class OperationsVerifier
    {
        private static BigInteger ConvertBigNumber(int[] number)
        {
            BigInteger bigNumber = 0;
            foreach (var digit in number)
            {
                bigNumber = bigNumber * 10 + digit;
            }

            return bigNumber;
        }

        public static BigInteger CalculatePow(BigInteger number, BigInteger exponent)
        {
            BigInteger result = 1;
            while (!exponent.IsZero)
            {
                result = BigInteger.Multiply(result, number);
                exponent = BigInteger.Subtract(exponent, 1);
            }

            return result;

        }
        private static bool CheckEqual(int[] numberArray, BigInteger number)
        {
            BigInteger convertedNumber = ConvertBigNumber(numberArray);
            return number.Equals(convertedNumber);
        }

        public static bool CheckAdd(int[] argument1, int[] argument2, int[] result)
        {
            BigInteger number1 = ConvertBigNumber(argument1);
            BigInteger number2 = ConvertBigNumber(argument2);
            BigInteger expectedResult = ConvertBigNumber(result);
            BigInteger actualResult = BigInteger.Add(number1, number2);
            return expectedResult.Equals(actualResult);
        }
        public static bool CheckSubtraction(int[] argument1, int[] argument2, int[] result)
        {
            BigInteger number1 = ConvertBigNumber(argument1);
            BigInteger number2 = ConvertBigNumber(argument2);
            BigInteger expectedResult = ConvertBigNumber(result);
            BigInteger actualResult = BigInteger.Subtract(number1,number2);
            return expectedResult.Equals(actualResult);
        }
        public static bool CheckMultiply(int[] argument1, int[] argument2, int[] result)
        {
            BigInteger number1 = ConvertBigNumber(argument1);
            BigInteger number2 = ConvertBigNumber(argument2);
            BigInteger expectedResult = ConvertBigNumber(result);
            BigInteger actualResult = BigInteger.Multiply(number1,number2);
            return expectedResult.Equals(actualResult);
        }
        public static bool CheckDivision(int[] argument1, int[] argument2, int[] result)
        {
            BigInteger number1 = ConvertBigNumber(argument1);
            BigInteger number2 = ConvertBigNumber(argument2);
            BigInteger expectedResult = ConvertBigNumber(result);
            BigInteger actualResult = BigInteger.Divide(number1,number2);
            return expectedResult.Equals(actualResult);
        }
        public static bool CheckPower(int[] argument1, int[] argument2, int[] result)
        {
            BigInteger number1 = ConvertBigNumber(argument1);
            BigInteger number2 = ConvertBigNumber(argument2);
            BigInteger expectedResult = ConvertBigNumber(result);
            BigInteger actualResult = CalculatePow(number1, number2);
            return expectedResult.Equals(actualResult);
        }
        public static bool CheckSquare(int[] argument1,int[] result)
        {
            BigInteger number1 = ConvertBigNumber(argument1);
            BigInteger expectedResult = ConvertBigNumber(result);
            BigInteger actualResult = number1.SquareRoot();
            return expectedResult.Equals(actualResult);
        }

        public static bool Smaller(int[] number1, int[] number2)
        {
            BigInteger firstNumber = ConvertBigNumber(number1);
            BigInteger secondNumber = ConvertBigNumber(number2);
            return BigInteger.Compare(firstNumber,secondNumber) < 0;
        }

        public static bool Greater(int[] number1, int[] number2)
        {
            BigInteger firstNumber = ConvertBigNumber(number1);
            BigInteger secondNumber = ConvertBigNumber(number2);
            return BigInteger.Compare(firstNumber, secondNumber) > 0;
        }

        public static bool Even(int[] number1, int[] number2)
        {
            BigInteger firstNumber = ConvertBigNumber(number1);
            BigInteger secondNumber = ConvertBigNumber(number2);
            return BigInteger.Compare(firstNumber, secondNumber) ==0;
        }

        public static bool CheckDivisible(int[] number1, int[] number2)
        {
            BigInteger firstNumber = ConvertBigNumber(number1);
            BigInteger secondNumber = ConvertBigNumber(number2);
            BigInteger result = BigInteger.Divide(firstNumber, secondNumber);
            BigInteger actualDivident = BigInteger.Multiply(result, secondNumber);
            return BigInteger.Compare(firstNumber, actualDivident) == 0;
        }
    }
}
