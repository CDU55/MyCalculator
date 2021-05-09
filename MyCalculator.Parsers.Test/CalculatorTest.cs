using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MyCalculator.Parsers.Test.CalculatorTestInputs;
using MyCalculator.Parsers.Exceptions;

namespace MyCalculator.Parsers.Test
{
    [TestFixture]
    public class CalculatorTest
    {

        private Calculator calculator;

        [SetUp]
        public void SetUp()
        {
            calculator = new Calculator();
        }

        // *** ADD ***
        [Test]
        [TestCaseSource(typeof(AddTestsInputs), nameof(AddTestsInputs.ValidAddInputs))]
        public void ShouldAddCorrectlyTheNumbersReceived(int[] firstNumber, int[] secondNumber, int[] expected)
        {
            var res = calculator.AddNumbers(firstNumber, secondNumber);
            Assert.AreEqual(res, expected);
        }

        [Test]
        [TestCaseSource(typeof(NullParameterTestInput), nameof(NullParameterTestInput.NullParameterInput))]
        public void ShouldThrowNullPointerExceptionWhenAdding(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<System.NullReferenceException>(() => calculator.AddNumbers(
                firstNumber, secondNumber));
        }


        // *** SUBTRACT ***
        [Test]
        [TestCaseSource(typeof(SubtractTestInputs), nameof(SubtractTestInputs.ValidSubtractInputs))]
        public void ShouldSubtractCorrectlyTheNumbersReceived(int[] firstNumber, int[] secondNumber, int[] expected)
        {
            var res = calculator.SubtractNumbers(firstNumber, secondNumber);
            Assert.AreEqual(res, expected);
        }

        [Test]
        [TestCaseSource(typeof(NullParameterTestInput), nameof(NullParameterTestInput.NullParameterInput))]
        public void ShouldThrowNullPointerExceptionWhenMultiplying(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<System.NullReferenceException>(() => calculator.SubtractNumbers(
                firstNumber, secondNumber));
        }

        // *** MULTIPLY ***
        [Test]
        [TestCaseSource(typeof(MultiplyTestInputs), nameof(MultiplyTestInputs.ValidMultiplyInputs))]
        public void ShouldMultiplyCorrectlyTheNumbersReceived(int[] firstNumber, int[] secondNumber, int[] expected)
        {
            var res = calculator.MultiplyNumbers(firstNumber, secondNumber);
            Assert.AreEqual(res, expected);
        }

        [Test]
        [TestCaseSource(typeof(NullParameterTestInput), nameof(NullParameterTestInput.NullParameterInput))]
        public void ShouldThrowNullPointerExceptionWhenSubtracting(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<System.NullReferenceException>(() => calculator.MultiplyNumbers(
                firstNumber, secondNumber));
        }

        // *** DIVIDE ***

        [Test]
        [TestCaseSource(typeof(DivisionTestInputs), nameof(DivisionTestInputs.ValidDivisionInputs))]
        public void ShouldDivideCorrectlyTheNumbersReceived(int[] firstNumber, int[] secondNumber, int[] expected)
        {
            var res = calculator.DivideNumbers(firstNumber, secondNumber);
            Assert.AreEqual(res, expected);
        }

        [Test]
        [TestCaseSource(typeof(DivisionTestInputs), nameof(DivisionTestInputs.NotDivisibleNumbers))]
        public void ShouldThrowNotDivisibleExceptionWhenDividing(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<NotDivisibleException>(() =>
            calculator.DivideNumbers(firstNumber, secondNumber) );
        }

        [Test]
        [TestCaseSource(typeof(NullParameterTestInput), nameof(NullParameterTestInput.NullParameterInput))]
        public void ShouldThrowNullPointerExceptionWhenDividing(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<System.NullReferenceException>(() => calculator.DivideNumbers(
                firstNumber, secondNumber));
        }

        // *** MOD ***

        [Test]
        [TestCaseSource(typeof(SupportTestFunctionInputs), nameof(SupportTestFunctionInputs.ModValidInputs))]
        public void ShouldCalculateModCorrectlyTheNumbersReceived(int[] firstNumber, int[] secondNumber, int[] expected)
        {
            var res = calculator.Mod(firstNumber, secondNumber);
            Assert.AreEqual(res, expected);
        }


        // *** IS_ZERO ***

        [Test]
        [TestCaseSource(typeof(SupportTestFunctionInputs), nameof(SupportTestFunctionInputs.IsZeroValidInputs))]
        public void ShouldAssertZeroCorrectlyTheNumberReceived(int[] firstNumber, bool expected)
        {
            var res = calculator.IsZero(firstNumber);
            if (expected)
            {
                Assert.IsTrue(res);
            }
            else {
                Assert.IsFalse(res);
            }
        }

        // *** IS_MULTIPLE_ZERO ***

        [Test]
        [TestCaseSource(typeof(SupportTestFunctionInputs), nameof(SupportTestFunctionInputs.IsMultipleZeroValidInputs))]
        public void ShouldAssertMultipleZeroCorrectlyTheNumberReceived(int[] firstNumber, bool expected)
        {
            var res = calculator.IsMultipleZero(firstNumber);
            if (expected)
            {
                Assert.IsTrue(res);
            }
            else
            {
                Assert.IsFalse(res);
            }
        }

        // *** POW ***

        [Test]
        [TestCaseSource(typeof(RaiseToPowerTestInputs), nameof(RaiseToPowerTestInputs.ValidPowInputs))]
        public void ShouldRaiseToPowerCorrectlyTheNumbersReceived(int[] firstNumber, int[] secondNumber, int[] expected)
        {
            var res = calculator.RaiseToPower(firstNumber, secondNumber);
            Assert.AreEqual(res, expected);
        }


        [Test]
        [TestCaseSource(typeof(RaiseToPowerTestInputs), nameof(RaiseToPowerTestInputs.InalidPowInputs))]
        public void ShouldThrowIndexOutOfRangeExceptionWhenRaisingToPower(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<System.IndexOutOfRangeException>(() => calculator.RaiseToPower(
                firstNumber, secondNumber));
        }


        // *** SQRT ***

        [Test]
        [TestCaseSource(typeof(SquareRootTestInputs), nameof(SquareRootTestInputs.ValidSqrtInputs))]
        public void ShouldCalculateSqrtCorrectlyTheNumbersReceived(int[] firstNumber, int[] expected)
        {
            var res = calculator.SquareRoot(firstNumber);
            Assert.AreEqual(res, expected);
        }


        [Test]
        [TestCaseSource(typeof(NullParameterTestInput), nameof(NullParameterTestInput.NullSingleParameterInput))]
        public void ShouldThrowNullPointerExceptionWhenCalculatingSQRT(int[] firstNumber)
        {

            Assert.Throws<System.NullReferenceException>(() => calculator.SquareRoot(
                firstNumber));
        }

        // *** Greater ***

        [Test]
        [TestCaseSource(typeof(CompareFuntionTestInputs), nameof(CompareFuntionTestInputs.ValidGreaterInputs))]
        public void ShouldAssertGreaterCorrectlyForTheNumbersReceived(int[] firstNumber, int[] secondNumber, bool expected)
        {
            var res = calculator.Greater(firstNumber, secondNumber);
            if (expected)
            {
                Assert.IsTrue(res);
            }
            else
            {
                Assert.IsFalse(res);
            }
        }


        [Test]
        [TestCaseSource(typeof(NullParameterTestInput), nameof(NullParameterTestInput.NullParameterInput))]
        public void ShouldThrowNullPointerExceptionWhenCalculatingGreater(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<System.NullReferenceException>(() => calculator.Greater(
                firstNumber, secondNumber));
        }

        // *** Equal ***

        [Test]
        [TestCaseSource(typeof(CompareFuntionTestInputs), nameof(CompareFuntionTestInputs.ValidEqualInputs))]
        public void ShouldAssertEqualCorrectlyForTheNumbersReceived(int[] firstNumber, int[] secondNumber, bool expected)
        {
            var res = calculator.Equal(firstNumber, secondNumber);
            if (expected)
            {
                Assert.IsTrue(res);
            }
            else
            {
                Assert.IsFalse(res);
            }
        }


        [Test]
        [TestCaseSource(typeof(NullParameterTestInput), nameof(NullParameterTestInput.NullParameterInput))]
        public void ShouldThrowNullPointerExceptionWhenCalculatingEqual(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<System.NullReferenceException>(() => calculator.Equal(
                firstNumber, secondNumber));
        }

        // *** Smaller ***

        [Test]
        [TestCaseSource(typeof(CompareFuntionTestInputs), nameof(CompareFuntionTestInputs.ValidLessInputs))]
        public void ShouldAssertSmallerCorrectlyForTheNumbersReceived(int[] firstNumber, int[] secondNumber, bool expected)
        {
            var res = calculator.Smaller(firstNumber, secondNumber);
            if (expected)
            {
                Assert.IsTrue(res);
            }
            else
            {
                Assert.IsFalse(res);
            }
        }


        [Test]
        [TestCaseSource(typeof(NullParameterTestInput), nameof(NullParameterTestInput.NullParameterInput))]
        public void ShouldThrowNullPointerExceptionWhenCalculatingSmaller(int[] firstNumber, int[] secondNumber)
        {

            Assert.Throws<System.NullReferenceException>(() => calculator.Smaller(
                firstNumber, secondNumber));
        }

        // *** Format result array ***
        [Test]
        [TestCaseSource(typeof(SupportTestFunctionInputs), nameof(SupportTestFunctionInputs.FormResultArrayValidInputs))]
        public void ShouldFormatCorrectlyTheNumberReceived(int[] firstNumber, int[] expected)
        {
            var res = calculator.FormatResultArray(firstNumber);
            Assert.AreEqual(res, expected);
        }

        // *** Valid array size ***
        [Test]
        [TestCaseSource(typeof(SupportTestFunctionInputs), nameof(SupportTestFunctionInputs.ValidArraySizeValidInputs))]
        public void ShouldValidateArraySizeCorrectlyTheNumberReceived(int[] firstNumber, int maxSize, bool expected)
        {
            var res = calculator.ValidArraySize(firstNumber, maxSize);
            if (expected)
            {
                Assert.IsTrue(res);
            }
            else
            {
                Assert.IsFalse(res);
            }
        }

    }
}
