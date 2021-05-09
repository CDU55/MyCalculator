using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Test
{
    public class ExtensionsTestInputs
    {
        public static object[] ArrayToStringInputs = {
            new object[] { new[] { 1, 2, 3 },"321"},
            new object[] { new[] {0, 1, 2, 3, },"3210"},
            new object[] { new[] { 1, 2, 3,-1 },"321"},
        };

        public static object[] StringToArrayInputs = {
            new object[] {"321", new[] {1,2,3}},
            new object[] {"100", new[] {0,0,1}},
            new object[] {"102", new[] {2,0,1}},
        };

        public static object[] ArrayToStringInputsException = {
            new object[] { new[] { 1, -2, 3 }},
            new object[] { new[] {0, 1, 20, 3, }},
        };

        public static object[] StringToArrayInputsExceptions = {
            new object[] {"321a"},
            new object[] {"-123"},
            new object[] {"1.5"}
        };
    }
}
