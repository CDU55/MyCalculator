using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class SubtractTestInputs
    {

        public static object[] ValidSubtractInputs = {
             new object[] { new int[1] { 1 },  new int[1] { 1 } , new int[1] { 0 } },
             new object[] { new int[2] { 1, 2 },  new int[1] { 2 } , new int[2] { 9, 1 } },
             new object[] { new int[4] { 0, 0, 0, 1 },  new int[1] { 1 } , new int[3] { 9, 9, 9 } },
             new object[] {
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                 new int[12] { 7, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 8 } ,
                 new int[12] { 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 }
             },
             new object[] { new int[2] { 7, 7 },  new int[1] { 0 } ,  new int[2] { 7, 7 } },
             new object[] { new int[1] { 0 },  new int[1] { 0 } ,  new int[1] { 0 } },
             new object[] { new int[1] { 8 }, new int[0] { } ,  new int[1] { 8 } },
             new object[] { new int[1] { 6 }, new int[1] { 7 } ,  new int[1] { 9 } }

        };

    }
}
