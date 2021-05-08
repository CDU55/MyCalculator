using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MyCalculator.Parsers.Exceptions;


namespace MyCalculator.Parsers.Test
{
    [TestFixture]
    public class PostfixConverterTest
    {
        private IPostfixConverter _converter;

        [SetUp]
        public void SetUp()
        {
            _converter = new PostfixConverter(new TokenValidator());
        }

        [Test]
        [TestCase("+",1)]
        [TestCase("-", 1)]
        [TestCase("*", 2)]
        [TestCase("/", 2)]
        [TestCase("^", 3)]
        [TestCase("#", 3)]
        public void ShouldReturnValidOperatorPrecedence(string providedOperator,int expectedPrecedence)
        {
            var actualPrecedence = _converter.GetPrecedence(providedOperator);
            Assert.AreEqual(actualPrecedence,expectedPrecedence);
        }
        [Test]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase(" ")]
        [TestCase("123")]
        public void ShouldThrowInvalidOperatorExceptionGePrecedence(string providedOperator)
        {
            Assert.Throws<InvalidOperatorException>(() => _converter.GetPrecedence(providedOperator));
        }

        [Test]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase(" ")]
        [TestCase("123")]
        public void ShouldThrowInvalidOperatorExceptionLeftAssociativeOperator(string providedOperator)
        {
            Assert.Throws<InvalidOperatorException>(() => _converter.IsLeftAssociative(providedOperator));
        }

        [Test]
        [TestCase("+")]
        [TestCase("-")]
        [TestCase("*")]
        [TestCase("/")]
        public void ShouldBeLeftAssociativeOperator(string providedOperator)
        {
            var leftAssociative = _converter.IsLeftAssociative(providedOperator);
            Assert.IsTrue(leftAssociative);
        }

        [Test]
        [TestCase("^")]
        [TestCase("#")]
        public void ShouldNotBeLeftAssociativeOperator(string providedOperator)
        {
            var leftAssociative = _converter.IsLeftAssociative(providedOperator);
            Assert.IsFalse(leftAssociative);
        }

        [Test]
        [TestCaseSource(typeof(PostfixConverterTestInputs),nameof(PostfixConverterTestInputs.SourceListsUnpairedParenthesisException))]
        public void ShouldThrowUnpairedParenthesisException(List<string> tokens)
        {
            Assert.Throws<UnpairedParenthesisException>(() => _converter.ConvertToPostfix(tokens));
        }

        [Test]
        [TestCaseSource(typeof(PostfixConverterTestInputs),nameof(PostfixConverterTestInputs.SourceListsPostfixConversion))]
        public void ShouldReturnCorrectPostfixNotation(List<string> tokens, List<string> expectedPostfix)
        {
            var actualPostfix = _converter.ConvertToPostfix(tokens);
            CollectionAssert.AreEqual(expectedPostfix, actualPostfix);
        }
    }
}
