using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class CompareFuntionTestInputs
    {
        public static object[] ValidGreaterInputs = {
             new object[] { new int[1] { 1 },  new int[1] { 1 } , false },
             new object[] { new int[1] { 9 },  new int[1] { 8 } , true },
             new object[] {
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                 new int[9] { 9, 9, 9, 9, 9, 9, 9, 9, 9 } ,
                 true
             },
             new object[] { new int[1] { 0 },  new int[3] { 9, 9, 9 } ,  false },
             new object[] { new int[2] { 7, 7 },  new int[1] { 0 } , true },
             new object[] { new int[0] {  },  new int[1] { 1 } , false },
             new object[] { new int[1] { 9 },  new int[0] {  } , true }
        };

        public static object[] ValidEqualInputs = {
             new object[] { new int[1] { 1 },  new int[1] { 1 } , true },
             new object[] { new int[1] { 9 },  new int[1] { 8 } , false },
             new object[] {
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                 new int[9] { 9, 9, 9, 9, 9, 9, 9, 9, 9 } ,
                 false
             },
             new object[] {
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 } ,
                 true
             },
             new object[] { new int[1] { 0 },  new int[3] { 9, 9, 9 } ,  false },
             new object[] { new int[2] { 7, 7 },  new int[1] { 0 } , false },
             new object[] { new int[0] {  },  new int[1] { 1 } , false },
             new object[] { new int[1] { 9 },  new int[0] {  } , false }
        };

        public static object[] ValidLessInputs = {
             new object[] { new int[1] { 1 },  new int[1] { 1 } , false },
             new object[] { new int[1] { 9 },  new int[1] { 8 } , false },
             new object[] { new int[1] { 5 },  new int[1] { 8 } , true },
             new object[] {
                 new int[12] { 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9, 9 },
                 new int[9] { 9, 9, 9, 9, 9, 9, 9, 9, 9 } ,
                 false
             },
             new object[] {
                 new int[7] { 9, 9, 9, 9, 9, 9, 9 },
                 new int[9] { 9, 9, 9, 9, 9, 9, 9, 9, 9 } ,
                 true
             },
             new object[] { new int[1] { 0 },  new int[3] { 9, 9, 9 } ,  true },
             new object[] { new int[2] { 7, 7 },  new int[1] { 0 } , false },
             new object[] { new int[0] {  },  new int[1] { 1 } , true },
             new object[] { new int[1] { 9 },  new int[0] {  } , false }
        };


    }
}
