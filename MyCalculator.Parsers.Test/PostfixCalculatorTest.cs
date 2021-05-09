using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using MyCalculator.Parsers.Exceptions;
using NUnit.Framework;

namespace MyCalculator.Parsers.Test
{
    [TestFixture]
    public class PostfixCalculatorTest
    {
        private Mock<ITokenizer> _tokenizerMock;
        private Mock<ICalculator> _calculatorMock;
        private Mock<IPostfixConverter> _postfixConverterMock;
        private IPostfixCalculator _postfixCalculator;
        [SetUp]
        public void SetUp()
        {
            _tokenizerMock = new Mock<ITokenizer>();
            _calculatorMock = new Mock<ICalculator>();
            _postfixConverterMock = new Mock<IPostfixConverter>();
            _postfixCalculator = new PostfixCalculator(new TokenValidator(), _calculatorMock.Object,_tokenizerMock.Object,
                _postfixConverterMock.Object);
        }

        [Test]
        [TestCaseSource(typeof(PostfixCalculatorTestInputs),nameof(PostfixCalculatorTestInputs.FromPostfixToInfixInputs))]
        public void ShouldConvertCorrectlyFromPostfixToInfix(List<string> input, string expected)
        {
            var actual = _postfixCalculator.FromPostfixToInfix(input);
            Assert.AreEqual(expected,actual);
        }

        [Test]
        [TestCaseSource(typeof(PostfixCalculatorTestInputs), nameof(PostfixCalculatorTestInputs.FromPostfixToInfixInputsInvalidExpressionException))]
        public void ShouldThrowInvalidExpressionException(List<string> input)
        {
            Assert.Throws<InvalidExpressionException>(()=>_postfixCalculator.FromPostfixToInfix(input));
        }

        #region CalculateFromPostfixTestsWithoutSteps
        [Test]
        public void ShouldCalculateCorrectlyFromPostfix1()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> {"123", "124", "+"};
            var expected = new[] {7, 4, 2};
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 3, 2, 1 }, new[] { 4, 2, 1 })).Returns(new[] { 7, 4, 2 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps,false);
            Assert.IsEmpty(steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]
        public void ShouldCalculateCorrectlyFromPostfix2()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "6", "+", "7", "-" };
            var expected = new[] { 1 };
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 6 })).Returns(new[] { 8 });
            _calculatorMock.Setup(x => x.SubtractNumbers(new[] { 8 }, new[] { 7 })).Returns(new[] { 1 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, false);
            Assert.IsEmpty(steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.SubtractNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]

        public void ShouldCalculateCorrectlyFromPostfix3()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "8", "+", "2", "/" };
            var expected = new[] { 5 };
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 8 })).Returns(new[] { 0,1 });
            _calculatorMock.Setup(x => x.DivideNumbers(new[] { 0,1 }, new[] { 2 })).Returns(new[] {5 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, false);
            Assert.IsEmpty(steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]

        public void ShouldCalculateCorrectlyFromPostfix4()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "4", "9", "3", "/", "*", "+" };
            var expected = new[] { 4,1 };
            _calculatorMock.Setup(x => x.DivideNumbers(new[] { 9 }, new[] { 3 })).Returns(new[] { 3 });
            _calculatorMock.Setup(x => x.MultiplyNumbers(new[] { 4 }, new[] { 3 })).Returns(new[] { 2,1 });
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 2,1 })).Returns(new[] { 4, 1 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(false);

            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, false);
            Assert.IsEmpty(steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.MultiplyNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]

        public void ShouldCalculateCorrectlyFromPostfix5()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "7", "5", "*", "+", "2", "^", "1", "/" };
            var expected = new[] { 9, 6, 3, 1 };
            _calculatorMock.Setup(x => x.DivideNumbers(new[] { 9, 6, 3, 1 }, new[] { 1 })).Returns(new[] { 9, 6, 3, 1 });
            _calculatorMock.Setup(x => x.MultiplyNumbers(new[] { 7 }, new[] { 5 })).Returns(new[] { 5, 3 });
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 5, 3 })).Returns(new[] { 7, 3 });
            _calculatorMock.Setup(x => x.RaiseToPower(new[] { 7,3 }, new[] { 2 })).Returns(new[] { 9, 6,3,1 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(false);

            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, false);
            Assert.IsEmpty(steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.MultiplyNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.RaiseToPower(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]

        public void ShouldCalculateCorrectlyFromPostfix6()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "9", "#", "+", "8", "^" };
            var expected = new[] { 5, 2, 6, 0, 9, 3 };
            _calculatorMock.Setup(x => x.SquareRoot(new[] { 9 })).Returns(new[] { 3 });
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 3 })).Returns(new[] { 5 });
            _calculatorMock.Setup(x => x.RaiseToPower(new[] { 5 }, new[] { 8 })).Returns(new[] { 5,2,6,0,9,3 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(false);
            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, false);
            Assert.IsEmpty(steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.RaiseToPower(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.SquareRoot(It.IsAny<int[]>()), Times.Once);
        }
        #endregion
        #region CalculateFromPostfixTestsWithSteps
        [Test]
        public void ShouldCalculateCorrectlyFromPostfixWithSteps1()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "123", "124", "+" };
            var expected = new[] { 7, 4, 2 };
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 3, 2, 1 }, new[] { 4, 2, 1 })).Returns(new[] { 7, 4, 2 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, false);
            Assert.IsEmpty(steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]
        public void ShouldCalculateCorrectlyFromPostfixWithSteps2()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "6", "+", "7", "-" };
            var expected = new[] { 1 };
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 6 })).Returns(new[] { 8 });
            _calculatorMock.Setup(x => x.SubtractNumbers(new[] { 8 }, new[] { 7 })).Returns(new[] { 1 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, true);
            var expectedSteps = new List<string>() {"(8-7)"};
            CollectionAssert.AreEqual(expectedSteps,steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.SubtractNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]

        public void ShouldCalculateCorrectlyFromPostfixWithSteps3()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "8", "+", "2", "/" };
            var expected = new[] { 5 };
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 8 })).Returns(new[] { 0, 1 });
            _calculatorMock.Setup(x => x.DivideNumbers(new[] { 0, 1 }, new[] { 2 })).Returns(new[] { 5 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, true);
            var expectedSteps = new List<string>() { "(10/2)" };
            CollectionAssert.AreEqual(expectedSteps, steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]

        public void ShouldCalculateCorrectlyFromPostfixWithSteps4()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "4", "9", "3", "/", "*", "+" };
            var expected = new[] { 4, 1 };
            _calculatorMock.Setup(x => x.DivideNumbers(new[] { 9 }, new[] { 3 })).Returns(new[] { 3 });
            _calculatorMock.Setup(x => x.MultiplyNumbers(new[] { 4 }, new[] { 3 })).Returns(new[] { 2, 1 });
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 2, 1 })).Returns(new[] { 4, 1 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(false);

            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, true);
            var expectedSteps = new List<string>() { "(3+(4*2))", "(12+2)" };
            CollectionAssert.AreEqual(expectedSteps, steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.MultiplyNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]

        public void ShouldCalculateCorrectlyFromPostfixWithSteps5()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "7", "5", "*", "+", "2", "^", "1", "/" };
            var expected = new[] { 9, 6, 3, 1 };
            _calculatorMock.Setup(x => x.DivideNumbers(new[] { 9, 6, 3, 1 }, new[] { 1 })).Returns(new[] { 9, 6, 3, 1 });
            _calculatorMock.Setup(x => x.MultiplyNumbers(new[] { 7 }, new[] { 5 })).Returns(new[] { 5, 3 });
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 5, 3 })).Returns(new[] { 7, 3 });
            _calculatorMock.Setup(x => x.RaiseToPower(new[] { 7, 3 }, new[] { 2 })).Returns(new[] { 9, 6, 3, 1 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(false);

            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, true);
            var expectedSteps = new List<string>() { "(((35+2)^2)/1)", "((37^2)/1)", "(1369/1)" };
            CollectionAssert.AreEqual(expectedSteps, steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.MultiplyNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.RaiseToPower(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
        }
        [Test]

        public void ShouldCalculateCorrectlyFromPostfixWithSteps6()
        {
            var steps = new List<string>() { "2+2" };
            var input = new List<string> { "2", "9", "#", "+", "8", "^" };
            var expected = new[] { 5, 2, 6, 0, 9, 3 };
            _calculatorMock.Setup(x => x.SquareRoot(new[] { 9 })).Returns(new[] { 3 });
            _calculatorMock.Setup(x => x.AddNumbers(new[] { 2 }, new[] { 3 })).Returns(new[] { 5 });
            _calculatorMock.Setup(x => x.RaiseToPower(new[] { 5 }, new[] { 8 })).Returns(new[] { 5, 2, 6, 0, 9, 3 });
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(false);
            var actual = _postfixCalculator.CalculateFromPostfix(input, out steps, true);
            var expectedSteps = new List<string>() { "((3+2)^8)", "(5^8)" };
            CollectionAssert.AreEqual(expectedSteps, steps);
            CollectionAssert.AreEqual(expected, actual);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.RaiseToPower(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.SquareRoot(It.IsAny<int[]>()), Times.Once);
        }
        #endregion
        [Test]
        [TestCaseSource(typeof(PostfixCalculatorTestInputs),nameof(PostfixCalculatorTestInputs.SetMaxArraySizeInputs))]
        public void ShouldSetMaxArraySizeCorrectly(string input,int expected)
        {
            PostfixCalculator.SetMaxArraySize(input);
            var actual = PostfixCalculator.GetMaxArraySize();
            Assert.AreEqual(expected,actual);
        }

        #region CalculateFromPostfixExceptions
        [Test]
        [TestCase("-1")]
        [TestCase("0")]
        [TestCase("1.5")]
        [TestCase("100000000000000000000000000000000000000000000000000000000")]
        [TestCase("abc")]
        public void ShouldThrowExceptionWhenInvalidMaxArraySizeIsSet(string input)
        {
            Assert.Throws<InvalidNumberSizeException>(() => PostfixCalculator.SetMaxArraySize(input));
        }

        [Test]
        public void ShouldThrowInvalidNumberSizeExceptionForToken()
        {
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(false);
            Assert.Throws<InvalidNumberSizeException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() {"2222"}, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(),It.IsAny<int>()), Times.Once);

        }

        [Test]
        public void ShouldThrowInvalidExpressionExceptionForSquareRoot()
        {
            Assert.Throws<InvalidExpressionException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "#" }, out _));
        }

        [Test]
        public void ShouldThrowNotSquareRootException()
        {
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            _calculatorMock.Setup(x => x.SquareRoot(It.IsAny<int[]>())).Returns(new[] {-1});
            Assert.Throws<NotSquareNumberException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "10","#" }, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()), Times.Once);
            _calculatorMock.Verify(x => x.SquareRoot(It.IsAny<int[]>()), Times.Once);


        }

        [Test]
        public void ShouldInvalidExpressionExceptionForBinaryOperator()
        {
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);
            Assert.Throws<InvalidExpressionException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "10", "+" }, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ShouldThrowInvalidNumberSizeExceptionForAddition()
        {
            _calculatorMock.SetupSequence(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(true)
                .Returns(false);
            Assert.Throws<InvalidNumberSizeException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "123","234","+" }, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()), Times.Exactly(3));
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);

        }

        [Test]
        public void ShouldThrowInvalidNegativeSubtractionResultExceptionForSubtraction()
        {
            _calculatorMock.SetupSequence(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(true);
            _calculatorMock.Setup(x => x.Smaller(It.IsAny<int[]>(), It.IsAny<int[]>(),It.IsAny<bool>())).Returns(true);
            Assert.Throws<NegativeSubtractionResultException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "123", "234", "-" }, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()), Times.Exactly(2));
            _calculatorMock.Verify(x => x.Smaller(It.IsAny<int[]>(), It.IsAny<int[]>(), It.IsAny<bool>()), Times.Once);
            _calculatorMock.Verify(x => x.SubtractNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Never);


        }

        [Test]
        public void ShouldThrowInvalidNumberSizeExceptionForMultiplication()
        {
            _calculatorMock.SetupSequence(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(true)
                .Returns(false);
            Assert.Throws<InvalidNumberSizeException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "12512", "146132", "*" }, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()), Times.Exactly(3));
            _calculatorMock.Verify(x => x.MultiplyNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);

        }

        [Test]
        public void ShouldThrowInvalidNumberSizeExceptionForRaiseToPower()
        {
            _calculatorMock.SetupSequence(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(true)
                .Returns(false);
            Assert.Throws<InvalidNumberSizeException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "12512", "146132", "^" }, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()), Times.Exactly(3));
            _calculatorMock.Verify(x => x.RaiseToPower(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Once);

        }

        [Test]
        public void ShouldThrownDivisionByZeroExceptionDivisionIsZero()
        {
            _calculatorMock.SetupSequence(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(true);
            _calculatorMock.Setup(x => x.Smaller(It.IsAny<int[]>(), It.IsAny<int[]>(), It.IsAny<bool>())).Returns(true);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(true);
            Assert.Throws<DivideByZeroException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "123", "0", "/" }, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()), Times.Exactly(2));
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Never);
            _calculatorMock.Verify(x => x.IsZero(It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.IsMultipleZero(It.IsAny<int[]>()), Times.Never);
        }

        [Test]
        public void ShouldThrownDivisionByZeroExceptionDivisionIsMultipleZero()
        {
            _calculatorMock.SetupSequence(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()))
                .Returns(true)
                .Returns(true);
            _calculatorMock.Setup(x => x.Smaller(It.IsAny<int[]>(), It.IsAny<int[]>(), It.IsAny<bool>())).Returns(true);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(true);

            Assert.Throws<DivideByZeroException>(() =>
                _postfixCalculator.CalculateFromPostfix(new List<string>() { "123", "0", "/" }, out _));
            _calculatorMock.Verify(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>()), Times.Exactly(2));
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()), Times.Never);
            _calculatorMock.Verify(x => x.IsZero(It.IsAny<int[]>()), Times.Once);
            _calculatorMock.Verify(x => x.IsMultipleZero(It.IsAny<int[]>()), Times.Once);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-1000)]
        public void ShouldThrowInvalidNumberSizeCalculate(int maxArraySize)
        {
            Assert.Throws<InvalidNumberSizeException>(() => _postfixCalculator.Calculate("2+2", out _, maxArraySize));
            _tokenizerMock.Verify(x => x.Tokenize(It.IsAny<string>()),Times.Once);
            _postfixConverterMock.Verify(x => x.ConvertToPostfix(It.IsAny<List<string>>()), Times.Once);
        }

        #endregion
        [Test]
        [TestCaseSource(typeof(PostfixCalculatorTestInputs),nameof(PostfixCalculatorTestInputs.CalculateInputs))]
        public void ShouldCalculateResultFromExpression(string expression, List<string> tokens, List<string> postfix,
            int[] expected,int[] methodCalls)
        {
            _tokenizerMock.Setup(x => x.Tokenize(expression)).Returns(tokens);
            _postfixConverterMock.Setup(x => x.ConvertToPostfix(tokens)).Returns(postfix);
            _calculatorMock.Setup(x => x.Smaller(It.IsAny<int[]>(), It.IsAny<int[]>(), It.IsAny<bool>())).Returns(false);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(),It.IsAny<int>())).Returns(true);

            _calculatorMock.Setup(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>())).Returns(expected);
            _calculatorMock.Setup(x => x.SubtractNumbers(It.IsAny<int[]>(), It.IsAny<int[]>())).Returns(expected);
            _calculatorMock.Setup(x => x.MultiplyNumbers(It.IsAny<int[]>(), It.IsAny<int[]>())).Returns(expected);
            _calculatorMock.Setup(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>())).Returns(expected);
            _calculatorMock.Setup(x => x.RaiseToPower(It.IsAny<int[]>(), It.IsAny<int[]>())).Returns(expected);
            _calculatorMock.Setup(x => x.SquareRoot(It.IsAny<int[]>())).Returns(expected);
            var actual = _postfixCalculator.Calculate(expression, out _, 100000);
            CollectionAssert.AreEqual(actual,expected);
            _tokenizerMock.Verify(x=>x.Tokenize(expression),Times.Once);
            _postfixConverterMock.Verify(x=>x.ConvertToPostfix(tokens),Times.Once);
            _calculatorMock.Verify(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()),Times.Exactly(methodCalls[0]));
            _calculatorMock.Verify(x => x.SubtractNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()),Times.Exactly(methodCalls[1]));
            _calculatorMock.Verify(x => x.MultiplyNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()),Times.Exactly(methodCalls[2]));
            _calculatorMock.Verify(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()),Times.Exactly(methodCalls[3]));
            _calculatorMock.Verify(x => x.RaiseToPower(It.IsAny<int[]>(), It.IsAny<int[]>()),Times.Exactly(methodCalls[4]));
            _calculatorMock.Verify(x => x.SquareRoot(It.IsAny<int[]>()),Times.Exactly(methodCalls[5]));

        }

        [Test]
        [TestCaseSource(typeof(PostfixCalculatorTestInputs), nameof(PostfixCalculatorTestInputs.CalculateInputsSteps))]
        public void ShouldWriteOutputTestParameter(string expression, List<string> tokens, List<string> postfix,
            int[] expected1,int[] expected2,List<string> expectedSteps)
        {
            var actualSteps = new List<string>();
            _tokenizerMock.Setup(x => x.Tokenize(expression)).Returns(tokens);
            _postfixConverterMock.Setup(x => x.ConvertToPostfix(tokens)).Returns(postfix);
            _calculatorMock.Setup(x => x.Smaller(It.IsAny<int[]>(), It.IsAny<int[]>(), It.IsAny<bool>())).Returns(false);
            _calculatorMock.Setup(x => x.IsZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.IsMultipleZero(It.IsAny<int[]>())).Returns(false);
            _calculatorMock.Setup(x => x.ValidArraySize(It.IsAny<int[]>(), It.IsAny<int>())).Returns(true);

            _calculatorMock.SetupSequence(x => x.AddNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()))
                .Returns(expected1)
                .Returns(expected2);
            _calculatorMock.SetupSequence(x => x.SubtractNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()))
                .Returns(expected1)
                .Returns(expected2);
            _calculatorMock.SetupSequence(x => x.MultiplyNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()))
                .Returns(expected1)
                .Returns(expected2); ;
            _calculatorMock.SetupSequence(x => x.DivideNumbers(It.IsAny<int[]>(), It.IsAny<int[]>()))
                .Returns(expected1)
                .Returns(expected2);
            _calculatorMock.SetupSequence(x => x.RaiseToPower(It.IsAny<int[]>(), It.IsAny<int[]>()))
                .Returns(expected1)
                .Returns(expected2); ;
            _calculatorMock.SetupSequence(x => x.SquareRoot(It.IsAny<int[]>()))
                .Returns(expected1)
                .Returns(expected2); ;
            var actual = _postfixCalculator.Calculate(expression, out actualSteps, 100000);
            _tokenizerMock.Verify(x => x.Tokenize(expression), Times.Once);
            _postfixConverterMock.Verify(x => x.ConvertToPostfix(tokens), Times.Once);
            CollectionAssert.AreEqual(expectedSteps,actualSteps);
        }

    }
}
