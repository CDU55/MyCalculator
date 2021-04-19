using System;
using System.Numerics;
using MyCalculator.Parsers;

namespace TestEnv
{
    class Program
    {
        static void Main(string[] args)
        {
            var number1 = "121";
            var number1Arr = number1.ConvertToArrayNumber();
            Calculator calc = new Calculator();
            var result = calc.SquareRoot(number1Arr);
            Console.WriteLine(result.ConvertToString());

        }
    }
}
