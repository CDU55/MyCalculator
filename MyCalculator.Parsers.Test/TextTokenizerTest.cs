using System.Collections.Generic;
using MyCalculator.Parsers.Exceptions;
using NUnit.Framework;

namespace MyCalculator.Parsers.Test
{
    public class TextTokenizerTest
    {
        private ITokenizer _tokenizer;

        [SetUp]
        public void SetUp()
        {
            _tokenizer = new TextTokenizer(new TokenValidator());
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

        [TestCaseSource(typeof(TextTokenizerTestInputs),nameof(TextTokenizerTestInputs.TokenizerInputs))]
        public void ShouldIdentifyTokensCorrectly(string input, List<string> expectedOutput)
        {
            var actualOutput = _tokenizer.Tokenize(input);
            CollectionAssert.AreEqual(actualOutput,expectedOutput);
        }
    }
}
