using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class SquareRootTestInputs
    {

        public static object[] ValidSqrtInputs = {
             new object[] { new int[2] { 6, 1 },  new int[1] { 4 } },
             new object[] { new int[1] { 0 },  new int[1] { -1 } },
             new object[] {
                 new int[8] { 5, 2, 0,8, 5, 8, 0, 3 },
                 new int[4] { 5,5,5,5 }
             },
             new object[] { new int[0] {  },  new int[1] { -1 } },
             new object[] { new int[1] { 1 },  new int[1] { 1 } },
             new object[] { new int[1] { 7 }, new int[1] { -1 } }
        };

   
    }
}
