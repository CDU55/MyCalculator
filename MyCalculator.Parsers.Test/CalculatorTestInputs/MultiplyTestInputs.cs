using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class MultiplyTestInputs
    {

        public static object[] ValidMultiplyInputs = {
             new object[] { new int[1] { 0 },  new int[1] { 0 } , new int[1] { 0 } },
             new object[] { new int[1] { 9 },  new int[1] { 0 } , new int[1] { 0 } },
             new object[] { new int[1] { 0 },  new int[1] { 7 } , new int[1] { 0 } },
             new object[] { new int[2] { 1, 2 },  new int[1] { 2 } , new int[2] { 2, 4 } },
             new object[] {
                 new int[7] { 9, 0, 0, 0, 0, 0, 1 },
                 new int[5] { 1, 0 , 0 ,0, 1  },
                 new int[11] { 9, 0, 0, 0, 9, 0, 1, 0, 0, 0, 1 }
             },
             new object[] { new int[2] { 7, 7 },  new int[1] { 1 }, new int[2] { 7, 7 } },
             new object[] { new int[1] { 1 },  new int[4] { 1, 2, 3, 4 }, new int[4] { 1, 2, 3, 4 } }
        };
    }
}
