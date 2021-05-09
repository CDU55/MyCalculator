using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test.CalculatorTestInputs
{
    class RaiseToPowerTestInputs
    {

        public static object[] ValidPowInputs = {
             new object[] { new int[1] { 7 },  new int[1] { 1 } , new int[1] { 7 } },
             new object[] { new int[1] { 9 },  new int[1] { 0 } , new int[1] { 1 } },
             new object[] { new int[1] { 1 },  new int[4] { 1, 2, 3, 4 } , new int[1] { 1 } },
             new object[] {
                 new int[3] { 4, 0, 2 },
                 new int[2] {  0, 1 } ,
                 new int[24] { 6, 7, 5, 6, 3, 1, 0, 3, 1, 3, 6, 4, 7, 0, 6 ,8, 2, 0, 5, 2, 8, 4, 2, 1}
           
             },
             new object[] { new int[0] {  },  new int[1] { 1 } , new int[0] { } }             
        };

        public static object[] InalidPowInputs = {
            new object[] { new int[1] { 9 },  new int[0] {  } }
        };
    }
}
