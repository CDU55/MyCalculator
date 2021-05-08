using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test
{
    public class TextTokenizerTestInputs
    {
        public static object[] TokenizerInputs = {
            new object[] {"123+124", new List<string> {"123", "+", "124"}},
            new object[] {"(   123   *   124    )", new List<string> { "(", "123", "*", "124", ")" } },
            new object[] {"12361231734 / 123651347234721243513247 * ", new List<string> { "12361231734", "/", "123651347234721243513247","*" } },
            new object[] {"123^124-6234326523++125", new List<string> {"123", "^","124", "-", "6234326523","+","+","125" } },
            new object[] {"4374572-#12123674324", new List<string> { "4374572", "-","#", "12123674324" } },
            new object[] {"(((123+124)))/56-67^5124", new List<string> {"(", "(", "(", "123", "+", "124",")", ")", ")","/","56","-","67","^","5124"} }
        };
    }
}
