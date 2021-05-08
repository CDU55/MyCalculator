using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test
{
    public class PostfixConverterTestInputs
    {
        public static object[] SourceListsUnpairedParenthesisException = {
            new object[] {new List<string> {"(","123", "+", "124"}},
            new object[] {new List<string> { "123", "*", "124", ")" } },
            new object[] {new List<string> { "(", "(", "12361231734", "/", "123651347234721243513247","*", ")" } },
            new object[] {new List<string> { "(","123", "^","124", "-", "6234326523","+","+","125", ")", ")" } },
            new object[] {new List<string> { "(", "(", "(", "(","4374572", "-","#", "12123674324", ")", ")", ")" } },
            new object[] {new List<string> {"(", "(", "(", "123", "+", "124",")", ")", ")","/","56","-","67","^","5124", ")" } },
            new object[] {new List<string> {"(",")",")"}},
            new object[] {new List<string> { "(","(","123", "*", "124", ")" } },
            new object[] {new List<string> { "(","(", "12361231734", "/", "123651347234721243513247","*", ")","+", "(", "12361231734", "/", "123651347234721243513247", "*", ")" } },
            new object[] {new List<string> { "(","123", "^","124",")", "-", "6234326523","+","+","125", ")", ")" } },
            new object[] {new List<string> { "(", "(","4374572", "-","#", "12123674324", ")", ")", ")",")" } },
            new object[] {new List<string> {"(", "(", "(", "123", "+", "124",")","-","5", ")","-","(","22","/","2",")" } },
            new object[] {new List<string> {")", "(", "(", "123", "+", "124",")","-","5", ")" } }
        };

        public static object[] SourceListsPostfixConversion = {
            new object[] {new List<string> {"22","+", "5", "-","7"},new List<string> {"22","5", "+", "7","-"}},
            new object[] {new List<string> {"25","^", "30", "/","(","5","*","10",")","+","30"},new List<string> { "25", "30", "^", "5","10","*","/","30","+"}},
            new object[] {new List<string> {"8","*", "(", "5","+","7",")"},new List<string> {"8","5", "7", "+","*"}},
            new object[] {new List<string> {"5","^", "6", "^","5","*","7","+","2"},new List<string> {"5","6", "5", "^","^","7","*","2","+"}},
            new object[] {new List<string> {"2","*", "5", "/","2"},new List<string> {"2","5", "*", "2","/"}},
            new object[] {new List<string> {"#","(","10","^", "10",")"},new List<string> {"10","10", "^", "#"}},
            new object[] {new List<string> {"(","128", "/", "8",")","^","(","17","-","5",")"},new List<string> {"128","8", "/", "17","5","-","^"}},
            new object[] {new List<string> {"#","(", "(", "11","+","5",")","*","16",")"},new List<string> { "11", "5", "+", "16","*","#"}},
            new object[] {new List<string> {"2","+", "5", "-","7","+","8","^","5"},new List<string> {"2","5", "+", "7","-","8","5","^","+"}},
            new object[] {new List<string> {"2","*", "(", "6","/","3","*","4",")"},new List<string> {"2","6", "3", "/","4","*","*"}},
        };
    }
}
