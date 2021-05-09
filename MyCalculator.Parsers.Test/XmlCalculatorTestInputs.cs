using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test
{
    public class XmlCalculatorTestInputs
    {
        public static object[] CalculateInputs = {
            new object[] { new List<string> { "2","3","+","4","+" }, new []{9},new List<string> { "(5+4)" } },
            new object[] { new List<string> { "2","3","+","5","-" }, new []{0},new List<string> { "(5-5)" } },
            new object[] { new List<string> { "4","2","^","4","/","#" }, new []{2},new List<string> { "(#(16/4))","(#4)" } },
            new object[] { new List<string> { "2","3","+","4","+","*","6" }, new []{3,6},new List<string> { "((5+4)*6)","(9*6)" } },
            new object[] { new List<string> { "5","5","5","*","*" }, new []{5,2,1},new List<string> { "(25*5)"} },
        };
    }
}
