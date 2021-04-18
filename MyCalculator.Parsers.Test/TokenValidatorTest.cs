using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using MyCalculator.Parsers;

namespace MyCalculator.Parsers.Test
{
    [TestFixture]
    public class TokenValidatorTest
    {
        private TokenValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new TokenValidator();
        }

        [TestCase("\\")]
        [TestCase("&")]
        [TestCase(",")]
        [TestCase(".")]
        [TestCase("?")]
        public void ShouldIdentifyInvalidTokens(string token)
        {
            var validationResult = _validator.IsValid(token);
            Assert.IsFalse(validationResult);
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        [TestCase("0")]
        [TestCase("+")]
        [TestCase("-")]
        [TestCase("/")]
        [TestCase("*")]
        [TestCase("^")]
        [TestCase("#")]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase(" ")]
        public void ShouldIdentifyValidTokens(string token)
        {
            var validationResult = _validator.IsValid(token);
            Assert.IsTrue(validationResult);
        }
        [TestCase("\\")]
        [TestCase("&")]
        [TestCase(",")]
        [TestCase(".")]
        [TestCase("?")]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        [TestCase("0")]
        [TestCase("(")]
        [TestCase(")")]
        [TestCase(" ")]

        public void ShouldIdentifyInvalidOperators(string token)
        {
            var validationResult = _validator.IsOperator(token);
            Assert.IsFalse(validationResult);
        }

        [TestCase("+")]
        [TestCase("-")]
        [TestCase("/")]
        [TestCase("*")]
        [TestCase("^")]
        [TestCase("#")]
        public void ShouldIdentifyValidOperators(string token)
        {
            var validationResult = _validator.IsOperator(token);
            Assert.IsTrue(validationResult);
        }

        [TestCase("*")]
        [TestCase("&")]
        [TestCase(",")]
        [TestCase(".")]
        [TestCase("?")]
        [TestCase("*")]
        [TestCase("&")]
        [TestCase(",")]
        [TestCase(".")]
        [TestCase("?")]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        [TestCase("0")]
        [TestCase(" ")]
        public void ShouldIdentifyInvalidParenthesis(string token)
        {
            var validationResult = _validator.IsParenthesis(token);
            Assert.IsFalse(validationResult);
        }

        [TestCase("(")]
        [TestCase(")")]
        public void ShouldIdentifyValidParenthesis(string token)
        {
            var validationResult = _validator.IsParenthesis(token);
            Assert.IsTrue(validationResult);
        }

        [TestCase("-1")]
        [TestCase("2.5")]
        [TestCase("3,6")]
        [TestCase("4!")]
        [TestCase("512353462347.3246")]
        [TestCase("-6123423523")]
        [TestCase("07623472457")]
        [TestCase("812346243725a3462437")]
        [TestCase("9+5")]
        [TestCase("12412352362366623645e")]

        public void ShouldIdentifyInvalidNumber(string token)
        {
            var validationResult = _validator.IsNumber(token);
            Assert.IsFalse(validationResult);
        }

        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        [TestCase("0")]
        [TestCase("12342154125123513261436134762347283")]
        [TestCase("21235123612346723472457254723458125")]
        [TestCase("31325123462347238456956045624361234738496703562347")]
        [TestCase("412513261347245873546234613784589659670789687905683567")]
        [TestCase("518236509813476081723857128390768913427689021347098")]
        [TestCase("61376176817348967093480916583249689123408609134870823467134")]
        [TestCase("7183275891734896723489067289034768243")]
        [TestCase("81893259183745689017348967349867")]
        [TestCase("918325798134768971349867342")]
        [TestCase("1320958901348690234897825968123498591823968193489134")]
        public void ShouldIdentifyValidNumber(string token)
        {
            var validationResult = _validator.IsNumber(token);
            Assert.IsTrue(validationResult);
        }

        [TestCase("*")]
        [TestCase("&")]
        [TestCase(",")]
        [TestCase(".")]
        [TestCase("?")]
        [TestCase("1")]
        [TestCase("2")]
        [TestCase("3")]
        [TestCase("4")]
        [TestCase("5")]
        [TestCase("6")]
        [TestCase("7")]
        [TestCase("8")]
        [TestCase("9")]
        [TestCase("0")]
        [TestCase("(")]
        [TestCase(")")]
        public void ShouldIdentifyInvalidSpace(string token)
        {
            var validationResult = _validator.IsSpace(token);
            Assert.IsFalse(validationResult);
        }

        [TestCase(" ")]
        public void ShouldIdentifyValidSpace(string token)
        {
            var validationResult = _validator.IsSpace(token);
            Assert.IsTrue(validationResult);
        }

    }
}
