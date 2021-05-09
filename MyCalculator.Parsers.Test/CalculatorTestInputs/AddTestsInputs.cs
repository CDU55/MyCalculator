using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class AddTestsInputs
    {

        public static object[] ValidAddInputs = {
             new object[] { new int[1] { 1 },  new int[1] { 1 } , new int[1] { 2 } },
             new object[] { new int[1] { 9 },  new int[1] { 9 } , new int[2] { 8, 1 } },
             new object[] {
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 } ,
                 new int[13] { 8, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 1 } 
             },
             new object[] { new int[1] { 0 },  new int[3] { 9, 9, 9 } ,  new int[3] { 9, 9, 9 } },
             new object[] { new int[2] { 7, 7 },  new int[1] { 0 } ,  new int[2] { 7, 7 } },
             new object[] { new int[0] {  },  new int[1] { 1 } , new int[1] { 1 } },
             new object[] { new int[1] { 9 },  new int[0] {  } , new int[1] { 9 } }
        };

    }
}
