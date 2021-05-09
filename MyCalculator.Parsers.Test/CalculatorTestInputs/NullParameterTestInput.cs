using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class NullParameterTestInput
    {

        public static object[] NullParameterInput = {
            new object[] { null,  new int[1] { 1 } },
            new object[] {  new int[1] { 1 }, null  },
            new object[] {  null, null }
        };

        public static object[] NullSingleParameterInput = {
            new object[] { null }
        };
    }
}
