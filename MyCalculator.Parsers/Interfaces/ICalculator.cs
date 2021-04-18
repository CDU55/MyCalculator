using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers
{
    public interface ICalculator
    {
        public bool IsSquareNumber(int[] number);
        public bool IsZero(int[] number);
        public bool IsDivisible(int[] firstNumber, int[] secondNumber);
        public int[] AddNumbers(int[] firstNumber, int[] secondNumber);
        public int[] SubtractNumbers(int[] firstNumber, int[] secondNumber);
        public int[] MultiplyNumbers(int[] firstNumber, int[] secondNumber);
        public int[] DivideNumbers(int[] firstNumber, int[] secondNumber);
        public int[] RaiseToPower(int[] firstNumber, int[] secondNumber);
        public int[] SquareRoot(int[] fistNumber);

        public bool Greater(int[] firstNumber, int[] secondNumber, bool reversed = true);
        public bool Equal(int[] firstNumber, int[] secondNumber);
        public bool Smaller(int[] firstNumber, int[] secondNumber, bool reversed = true);
    }
}
