using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test
{
    public class PostfixCalculatorTestInputs
    {
        public static object[] FromPostfixToInfixInputs = {
            new object[] { new List<string> { "123", "124", "+" },"(123+124)" },
            new object[] { new List<string> { "2", "6", "+","7","-" },"((2+6)-7)" },
            new object[] { new List<string> { "2", "8", "+","2","/" },"((2+8)/2)" },
            new object[] { new List<string> { "2", "4", "9","3","/","*","+" }, "(2+(4*(9/3)))" },
            new object[] { new List<string> { "2", "7", "5","*","+","9","^","1","/" },"(((2+(7*5))^9)/1)" },
            new object[] { new List<string> { "2", "9", "#","+","8","^" },"((2+(#9))^8)" },
        };

        public static object[] FromPostfixToInfixInputsInvalidExpressionException = {
            new object[] { new List<string> { "123", "124" } },
            new object[] { new List<string> { "2", "+","7","-" } },
            new object[] { new List<string> { "2", "8", "+","2","/","/" } },
            new object[] { new List<string> { "2", "4", "9","3","/","*" } },
            new object[] { new List<string> { "2", "7", "5","*","+","9" } },
            new object[] { new List<string> { "9", "#","+","8","^" } },
        };

        public static object[] SetMaxArraySizeInputs = {
            new object[] { "1",1 },
            new object[] { "1000",1000 },
            new object[] { Int32.MaxValue.ToString(),Int32.MaxValue }
        };

        public static object[] CalculateInputs = {
            new object[] {"123 + 124",new List<string> {"123","+","124"},new List<string>{"123","124","+"},new[]{7,4,2},new[]{1,0,0,0,0,0}},
            new object[] {"124 - 123",new List<string> {"124","-","123"},new List<string>{"124","123","-"},new[]{1},new[]{0,1,0,0,0,0}},
            new object[] {"16 * 4",new List<string> {"16","*","4"},new List<string>{"16","4","*"},new[]{4,6},new[]{0,0,1,0,0,0}},
            new object[] {"16 / 4",new List<string> {"16","/","4"},new List<string>{"16","4","/"},new[]{4},new[]{0,0,0,1,0,0}},
            new object[] {"10 ^ 3",new List<string> {"10","^","3"},new List<string>{"10","3","^"},new[]{0,0,0,1},new[]{0,0,0,0,1,0}},
            new object[] {"#9",new List<string> {"#","9"},new List<string>{"9","#"},new[]{3},new[]{0,0,0,0,0,1}},
            new object[] {"3 * 3",new List<string> {"3","*","3"},new List<string>{"3","3","*"},new[]{9},new[]{0,0,1,0,0,0}},
            new object[] {"3 * 3",new List<string> {"3","*","3"},new List<string>{"3","3","*"},new[]{-1,9},new[]{0,0,1,0,0,0}},
        };

        public static object[] CalculateInputsSteps = {
            new object[] {"2 + 3 + 4",new List<string> {"2","+","3","+","4"},new List<string>{"2","3","+","4","+"}
                ,new[]{5},new[]{9},new List<string>{"(5+4)"}},

            new object[] {"2 + 3 * 9",new List<string> {"2","+","3","*","9"},new List<string>{"2","3","+","9","*"}
            ,new[]{6},new[]{3,6},new List<string>{"(6*9)"}},

            new object[] {"2 + 3 ^ 4",new List<string> {"2","+","3","^","4"},new List<string>{"2","3","+","4","^"}
                ,new[]{5},new[]{5,2,6},new List<string>{"(5^4)"}},

            new object[] {"2 + 3 / 5",new List<string> {"2","+","3","/","5"},new List<string>{"2","3","+","5","/"}
                ,new[]{5},new[]{1},new List<string>{"(5/5)"}},

            new object[] {"#(6 + 3)",new List<string> {"#","(","6","+","3",")"},new List<string>{"6","3","+","#"}
                ,new[]{9},new[]{3},new List<string>{"(#9)"}}
        };
    }
}
