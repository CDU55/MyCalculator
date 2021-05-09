using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class DivisionTestInputs
    {

        public static object[] ValidDivisionInputs = {
             new object[] { new int[1] { 8 },  new int[1] { 1 } , new int[1] { 8 } },
             new object[] { new int[4] { 9, 9, 9, 9 }, new int[4] { 9, 9, 9, 9 }, new int[1] {  1 } },
             new object[] {
                 new int[10] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
                 new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 2 } ,
                   new int[1] { 5 }
             },
             new object[] { new int[1] { 0 },  new int[3] { 9, 9, 9 } ,  new int[1] { 0 } },
             new object[] { new int[0] {  },  new int[1] { 1 } , new int[1] { 0 } }
        };


        public static object[] NotDivisibleNumbers = {
            new object[] { new int[2] { 0, 1 } , new int[1] { 7 } }
        };
    }
}
