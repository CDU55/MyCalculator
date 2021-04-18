using System;
using System.Collections.Generic;
using System.Text;
using MyCalculator.Parsers.Exceptions;
using NUnit.Framework;

namespace MyCalculator.Parsers.Test
{
    public class TextTokenizerTest
    {
        private ITokenizer _tokenizer;
        public static object[] _sourceLists;
        [SetUp]
        public void SetUp()
        {
            _tokenizer = new TextTokenizer(new TokenValidator());
            _sourceLists = new object[]
            {
                new object[] {"123+124", new List<string> {"123", "+", "124"}}
            };
        }

        [Test]
        [TestCase("123,+122")]
        [TestCase("123,+122")]
        [TestCase("123;122")]
        [TestCase("123 [ 122")]
        [TestCase("123\\122")]
        [TestCase("123!122")]
        [TestCase("123,+122")]
        [TestCase("123+122e")]
        [TestCase("#e^2")]
        [TestCase("999999i")]
        public void ShouldThrowInvalidTokenException(string input)
        {
            Assert.Throws<InvalidTokenException>(() => _tokenizer.Tokenize(input));
        }

        [TestCaseSource(nameof(_sourceLists))]
        public void ShouldIdentifyTokensCorrectly(string input, List<string> expectedOutput)
        {
            var actualOutput = _tokenizer.Tokenize(input);
            CollectionAssert.AreEqual(actualOutput,expectedOutput);
        }
    }
}
