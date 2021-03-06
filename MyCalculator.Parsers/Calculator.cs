using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using MyCalculator.Parsers.CodeContractsUitls;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers
{
    public class Calculator : ICalculator
    {

        public int[] SubtractNumbers(int[] firstNumber, int[] secondNumber)
        {
            Contract.Assert(firstNumber!=null && firstNumber.Length>0);
            Contract.Assert(secondNumber!=null && secondNumber.Length!=0);
            Contract.Ensures(OperationsVerifier.CheckSubtraction(firstNumber,secondNumber,Contract.Result<int[]>()));
            int maxLen = firstNumber.Length;
            int minLen = secondNumber.Length;
            int carriage = 0;
            var  result = new int[maxLen];

            for (int count = 0; count < minLen; count++)
            {
                Contract.Assert(carriage == 0 || carriage == -1);
                if (firstNumber[count] + carriage >= secondNumber[count])
                {
                    result[count] = firstNumber[count] - secondNumber[count] + carriage;
                    carriage = 0;
                }
                else
                {
                    result[count] = firstNumber[count] + 10 - secondNumber[count] + carriage;
                    carriage = -1;
                }
            }

            for (int countSecond = minLen; countSecond < maxLen; countSecond++)
            {
                Contract.Assert(carriage == 0 || carriage == -1);
                if ((firstNumber[countSecond] + carriage) >= 0)
                {
                    result[countSecond] = firstNumber[countSecond] + carriage;
                    carriage = 0;
                }
                else
                {
                    result[countSecond] = firstNumber[countSecond] + 10 + carriage;
                    carriage = -1;
                }
            }

            return FormatResultArray(result);
        }

        public int[] AddNumbers(int[] firstNumber, int[] secondNumber)
        {
            Contract.Assert(firstNumber != null && firstNumber.Length > 0);
            Contract.Assert(secondNumber != null && secondNumber.Length != 0);
            Contract.Ensures(OperationsVerifier.CheckAdd(firstNumber, secondNumber, Contract.Result<int[]>()));
            int[] maxNumber = firstNumber;

            int maxLen = firstNumber.Length;
            if (firstNumber.Length < secondNumber.Length)
            {
                maxLen = secondNumber.Length;
                maxNumber = secondNumber;
            }

            var result = new int[maxLen];

            int minLen = firstNumber.Length;
            if (firstNumber.Length > secondNumber.Length)
            {
                minLen = secondNumber.Length;
            }

            int carriage = 0;
            for (int count = 0; count < minLen; count++)
            {
                Contract.Assert(carriage >= 0);
                result[count] = firstNumber[count] + secondNumber[count] + carriage;
                if (result[count] > 9)
                {
                    carriage = result[count] / 10;
                    result[count] = result[count] % 10;
                }
                else
                {
                    carriage = 0;
                }
            }
            for (int countSecond = minLen; countSecond < maxLen; countSecond++)
            {
                Contract.Assert(carriage >= 0);
                result[countSecond] = maxNumber[countSecond] + carriage;
                if (result[countSecond] > 9)
                {
                    carriage = result[countSecond] / 10;
                    result[countSecond] = result[countSecond] % 10;
                }
                else
                {
                    carriage = 0;
                }
            }

            if (carriage != 0)
            {
                int[] resultWithCarriage = new int[maxLen + 1];
                Array.Copy(result, resultWithCarriage, result.Length);
                resultWithCarriage[maxLen] = carriage;
                return resultWithCarriage;
            }

            return result;
        }

        public int[] MultiplyNumbers(int[] firstNumber, int[] secondNumber)
        {
            Contract.Assert(firstNumber != null && firstNumber.Length > 0);
            Contract.Assert(secondNumber != null && secondNumber.Length != 0);
            Contract.Ensures(OperationsVerifier.CheckMultiply(firstNumber, secondNumber, Contract.Result<int[]>()));
            var result = new int[firstNumber.Length + secondNumber.Length];
            int carriage = 0;
            for (int countSecondNumber = 0; countSecondNumber < secondNumber.Length; countSecondNumber++)
            {
                Contract.Assert(carriage >= 0);
                for (int countFirstNumber = 0; countFirstNumber < firstNumber.Length; countFirstNumber++)
                {
                    result[countFirstNumber + countSecondNumber] += secondNumber[countSecondNumber] * firstNumber[countFirstNumber] + carriage;
                    if (result[countFirstNumber + countSecondNumber] > 9)
                    {
                        carriage = result[countFirstNumber + countSecondNumber] / 10;
                        result[countFirstNumber + countSecondNumber] = result[countFirstNumber + countSecondNumber] % 10;
                    }
                    else
                    {
                        carriage = 0;
                    }
                }
                if (carriage != 0)
                {
                    result[firstNumber.Length + countSecondNumber] += carriage;
                    carriage = 0;
                }
            }

            return FormatResultArray(result);
        }

        public int[] DivideNumbers(int[] firstNumber, int[] secondNumber)
        {
            Contract.Assert(firstNumber != null && firstNumber.Length > 0);
            Contract.Assert(secondNumber != null && secondNumber.Length != 0);
            Contract.Ensures(OperationsVerifier.CheckDivision(firstNumber, secondNumber, Contract.Result<int[]>()));
            Contract.EnsuresOnThrow<NotDivisibleException>(!OperationsVerifier.CheckDivisible(firstNumber,secondNumber));
            int skip = 0;
            int take = secondNumber.Length;
            var result = new int[0];
            if (IsMultipleZero(firstNumber))
            {
                return new int[] {0};
            }
            var remainder = new int[0];
            var reversedFirstNumber = firstNumber.Reverse().ToArray();
            var reversedSecondNumber = secondNumber.Reverse().ToArray();
            int[] temporaryDivident;
            while (skip < firstNumber.Length)
            {
                var currentSlice=reversedFirstNumber.Skip(skip).Take(take).ToArray();
                if (!IsZero(remainder))
                {
                    temporaryDivident = new int[currentSlice.Length + remainder.Length];
                    remainder.CopyTo(temporaryDivident, 0);
                    currentSlice.CopyTo(temporaryDivident, remainder.Length);
                }
                else
                {
                    temporaryDivident = currentSlice;
                }

                while (Greater(reversedSecondNumber, temporaryDivident))
                {
                    take++;
                    currentSlice = reversedFirstNumber.Skip(skip).Take(take).ToArray();
                    if (!IsZero(remainder))
                    {
                        temporaryDivident = new int[currentSlice.Length + remainder.Length];
                        remainder.CopyTo(temporaryDivident, 0);
                        currentSlice.CopyTo(temporaryDivident, remainder.Length);
                    }
                    else
                    {
                        temporaryDivident = currentSlice;
                    }

                    if (skip + take >= reversedFirstNumber.Length)
                    {
                        break;
                    }
                }

                if (Smaller(temporaryDivident, reversedSecondNumber,false) && !IsMultipleZero(temporaryDivident))
                {
                    throw new NotDivisibleException(
                        $"{firstNumber.ConvertToString()} is not divisible by {secondNumber.ConvertToString()}");
                }
                var currentResult = DivideNumbersSubtraction(temporaryDivident.Reverse().ToArray(), secondNumber,out remainder).Reverse().ToArray();
                remainder = remainder.Reverse().ToArray();
                var temporaryResult = new int[result.Length + currentResult.Length];
                result.CopyTo(temporaryResult,0);
                currentResult.CopyTo(temporaryResult,result.Length);
                result = temporaryResult;
                skip += take;
                take += (secondNumber.Length - remainder.Length);
                if (skip + take > reversedSecondNumber.Length)
                {
                    take = reversedSecondNumber.Length - skip;
                }
            }

            if (!IsZero(remainder))
            {
                throw new NotDivisibleException(
                    $"{firstNumber.ConvertToString()} is not divisible by {secondNumber.ConvertToString()}");
            }

            return result.Reverse().ToArray();
        }
        public int[] DivideNumbersSubtraction(int[] firstNumber, int[] secondNumber,out int[] remainder)
        {
            Contract.Assert(firstNumber != null && firstNumber.Length > 0);
            Contract.Assert(secondNumber != null && secondNumber.Length != 0);
            Contract.Ensures(OperationsVerifier.CheckDivision(firstNumber, secondNumber, Contract.Result<int[]>()));
            int[] result = new int[firstNumber.Length] ;
            while (Greater(firstNumber,secondNumber) || Equal(firstNumber,secondNumber))
            {
                firstNumber = SubtractNumbers(firstNumber, secondNumber);
                result = AddNumbers(result, new int[] {1});
            }

            remainder = FormatResultArray(firstNumber);
            return FormatResultArray(result);
        }

        public int[] Mod(int[] firstNumber, int[] secondNumber)
        {
            int[] result = new int[firstNumber.Length];
            while (Greater(firstNumber, secondNumber) || Equal(firstNumber,secondNumber))
            {
                firstNumber = SubtractNumbers(firstNumber, secondNumber);
            }

            return firstNumber;
        }

        public bool IsZero(int[] number)
        {
            Contract.Assert(number != null && number.Length==1);

            if (number.Length == 1 && number[0] == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
          
        }

        public bool IsMultipleZero(int[] number)
        {
            Contract.Assert(number!=null);
            Contract.Ensures((Contract.Result<bool>() == true && number.All(digit => digit == 0))
                             || (Contract.Result<bool>() == false && number.Any(digit => digit != 0)));
            for (int count = number.Length - 1; count >= 0; count--)
            {
                if (number[count] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public int[] RaiseToPower(int[] firstNumber, int[] secondNumber)
        {
            Contract.Assert(firstNumber != null && firstNumber.Length > 0);
            Contract.Assert(secondNumber != null && secondNumber.Length != 0);
            Contract.Ensures(OperationsVerifier.CheckPower(firstNumber, secondNumber, Contract.Result<int[]>()));
            if (IsZero(secondNumber))
            {
                return new int[1] { 1 };
            }
            if (secondNumber.Length == 1 && secondNumber[0] == 1)
            {
                return firstNumber;
            }

            int[] temp = MultiplyNumbers(firstNumber, firstNumber);
            if (secondNumber[0] % 2 == 0)
            {
                return RaiseToPower(temp, DivideNumbers(secondNumber,new int[]{2}));
            }
            else
            {
                return MultiplyNumbers(firstNumber, RaiseToPower(temp,DivideNumbers(SubtractNumbers(secondNumber,new []{1}),new []{2})));
            }
        }

        public int[] SquareRoot(int[] number)
        {
            Contract.Assert(number != null && number.Length > 0);
            Contract.Ensures(OperationsVerifier.CheckSquare(number, Contract.Result<int[]>()));
            int[] one = new[] { 1 };
            int[] left = new[] { 1 }, right = number;

            while (Smaller(left,right) || Equal(left,right))
            {
                var sum = AddNumbers(left, right);
                if (sum[0] % 2 == 1)
                {
                    sum = SubtractNumbers(sum, new int[] {1});
                }
                int[] mid = DivideNumbers(sum, new []{2});

                // Check if mid is perfect
                // square
                if (Equal(MultiplyNumbers(mid, mid), number))
                {
                    return mid;
                }

                // Mid is small -> go right to
                // increase mid
                if (Smaller(MultiplyNumbers(mid, mid), number))
                {
                    left = AddNumbers(mid, one);
                }

                // Mid is large -> to left
                // to decrease mid
                else
                {
                    right = SubtractNumbers(mid, one);
                }
            }
            return new[] { -1 };
        }

        public bool Greater(int[] firstNumber, int[] secondNumber,bool reversed=true)
        {
            Contract.Assert(firstNumber != null && firstNumber.Length != 0);
            Contract.Assert(secondNumber != null && secondNumber.Length != 0);
            Contract.Ensures((Contract.Result<bool>() == true && OperationsVerifier.Greater(firstNumber, secondNumber))
                             || (Contract.Result<bool>() == false && !OperationsVerifier.Greater(firstNumber, secondNumber)));
            if (firstNumber.Length > secondNumber.Length)
            {
                return true;
            }
            else if (firstNumber.Length < secondNumber.Length)
            {
                return false;
            }

            var digitIndexRange = Enumerable.Range(0, firstNumber.Length);
            if (reversed)
            {
                digitIndexRange = digitIndexRange.Reverse();
            }

            foreach (var digitIndex in digitIndexRange)
            {
                if (firstNumber[digitIndex] > secondNumber[digitIndex])
                {
                    return true;
                }
                else if (firstNumber[digitIndex] < secondNumber[digitIndex])
                {
                    return false;
                }
            }

            return false;
        }

        public bool Equal(int[] firstNumber, int[] secondNumber)
        {
            Contract.Assert(firstNumber != null && firstNumber.Length != 0);
            Contract.Assert(secondNumber != null && secondNumber.Length != 0);
            Contract.Ensures((Contract.Result<bool>() == true && OperationsVerifier.Even(firstNumber, secondNumber))
                             || (Contract.Result<bool>() == false && !OperationsVerifier.Even(firstNumber, secondNumber)));
            if (firstNumber.Length != secondNumber.Length)
            {
                return false;
            }

            for (int digitIndex = 0; digitIndex < firstNumber.Length; digitIndex++)
            {
                if (firstNumber[digitIndex] != secondNumber[digitIndex])
                {
                    return false;
                }
            }

            return true;
        }

        public bool Smaller(int[] firstNumber, int[] secondNumber,bool reversed=true)
        {
            Contract.Assert(firstNumber!=null && firstNumber.Length!=0);
            Contract.Assert(secondNumber!=null && secondNumber.Length!=0);
            Contract.Ensures((Contract.Result<bool>() == true && OperationsVerifier.Smaller(firstNumber,secondNumber))
                             || (Contract.Result<bool>() == false && !OperationsVerifier.Smaller(firstNumber, secondNumber)));
            if (firstNumber.Length < secondNumber.Length)
            {
                return true;
            }
            else if (firstNumber.Length > secondNumber.Length)
            {
                return false;
            }

            var digitIndexRange = Enumerable.Range(0, firstNumber.Length);
            if (reversed)
            {
                digitIndexRange = digitIndexRange.Reverse();
            }

            foreach (var digitIndex in digitIndexRange)
            {
                if (firstNumber[digitIndex] < secondNumber[digitIndex])
                {
                    return true;
                }
                else if (firstNumber[digitIndex] > secondNumber[digitIndex])
                {
                    return false;
                }
            }
            return false;
        }


        public int[] FormatResultArray(int[] result)
        {
            Contract.Assert(result!=null);
            Contract.Ensures(Contract.OldValue(result).Length == result.Length
                             || (Contract.OldValue(result).Length > result.Length && Contract.OldValue(result).Skip(result.Length).All(digit => digit == 0)));
            // if the number is zero return an array with only a digit
            if (IsMultipleZero(result))
            {
                return new int[1];
            }

            // remove the zero at the end
            if (result[result.Length - 1] == 0)
            {
                for (int firstDigitPositionCount = result.Length - 1; firstDigitPositionCount >= 0; firstDigitPositionCount--)
                {
                    if (result[firstDigitPositionCount] != 0)
                    {
                        int[] resultWithoutZeros = new int[firstDigitPositionCount + 1];
                        Array.Copy(result, resultWithoutZeros, firstDigitPositionCount + 1);
                        return resultWithoutZeros;
                    }
                }
            }
            
            return result;
        }

        public bool ValidArraySize(int[] number, int maxArraySize)
        {
            return number.Length <= maxArraySize;
        }
    }
}
