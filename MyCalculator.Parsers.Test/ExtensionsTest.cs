using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace MyCalculator.Parsers.Test
{
    [TestFixture]
    public class ExtensionsTest
    {
        [Test]
        [TestCaseSource(typeof(ExtensionsTestInputs),nameof(ExtensionsTestInputs.ArrayToStringInputs))]
        public void ShouldPerformValidConversionFromArrayToString(int[] input, string expected)
        {
            var actual = input.ConvertToString();
            Assert.AreEqual(expected,actual);
        }

        [Test]
        [TestCaseSource(typeof(ExtensionsTestInputs), nameof(ExtensionsTestInputs.StringToArrayInputs))]
        public void ShouldPerformValidConversionFromStringToArray(string input, int[] expected)
        {
            var actual = input.ConvertToArrayNumber();
            CollectionAssert.AreEqual(expected,actual);
        }

    }
}
