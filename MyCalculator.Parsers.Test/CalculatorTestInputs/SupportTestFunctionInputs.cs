using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class SupportTestFunctionInputs
    {

        public static object[] ModValidInputs = {
             new object[] { new int[1] { 1 },  new int[1] { 1 } , new int[1] { 0 } },
             new object[] { new int[1] { 9 },  new int[1] { 5 } , new int[1] { 4 } },
             new object[] {
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                 new int[8] { 5, 5, 5, 5, 5, 5, 5, 5 } ,
                 new int[4] { 9, 9, 9, 9 }
             },
             new object[] { new int[1] { 0 },  new int[3] { 9, 9, 9 } ,  new int[1] { 0 } },
             new object[] { new int[0] {  },  new int[1] { 1 } , new int[0] { } }
          //   new object[] { new int[1] { 9 },  new int[1] { 0 } , new int[1] { 9 } }, mod zero is not defined
        };

        public static object[] IsZeroValidInputs = {
             new object[] { new int[1] { 0 }, true },
             new object[] { new int[3] { 0, 0, 0 },  false },
             new object[] { new int[1] { 1 }, false },
             new object[] {
                 new int[12] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                false
             },
             new object[] { new int[0] {  },  false },
             new object[] { new int[6] { 0, 0, 0, 0, 0, 1 },  false }
        };

        public static object[] IsMultipleZeroValidInputs = {
             new object[] { new int[1] { 0 }, true },
             new object[] { new int[3] { 0, 0, 0 },  true },
             new object[] { new int[1] { 1 }, false },
             new object[] {
                 new int[12] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                true
             },
             new object[] { new int[0] {  },  true },
             new object[] { new int[6] { 0, 0, 0, 0, 0, 1 },  false }
        };

        public static object[] FormResultArrayValidInputs = {
             new object[] { new int[1] { 0 }, new int[1] { 0 } },
             new object[] { new int[3] { 0, 0, 0 },  new int[1] { 0 } },
             new object[] { new int[3] { 0, 0, 1 }, new int[3] { 0, 0, 1 } },
             new object[] {
                 new int[12] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
                 new int[6] {0, 0, 0, 0, 0, 1 },
             },
             new object[] {
                 new int[12] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                 new int[1] {0 },
             },
             new object[] { new int[0] {  },  new int[1] { 0 } },
        };

        public static object[] ValidArraySizeValidInputs = {
             new object[] { new int[1] { 0 }, 1, true },
             new object[] { new int[3] { 0, 0, 0 },  5, true },
             new object[] { new int[3] { 0, 0, 1 }, 2, false },
             new object[] {
                 new int[12] {0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1 },
                 15,
                 true
             },
             new object[] {
                 new int[12] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                 5,
                 false
             },
             new object[] { new int[0] {  }, 4, true },
        };
    }
}
