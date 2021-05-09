using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using MyCalculator.Parsers.Interfaces;
using NUnit.Framework;

namespace MyCalculator.Parsers.Test
{
    [TestFixture]
    public class XmlCalculatorTest
    {
        private Mock<IPostfixCalculator> _postfixCalculatorMock;
        private Mock<IXmlTokenizer> _xmlTokenizerMock;
        private XmlCalculator _xmlCalculator;
        public delegate void WriteSteps(List<string> postfix, out List<string> steps, bool convertSteps = true);

        [SetUp]
        public void SetUp()
        {
            _postfixCalculatorMock = new Mock<IPostfixCalculator>();
            _xmlTokenizerMock = new Mock<IXmlTokenizer>();
            _xmlCalculator = new XmlCalculator(_xmlTokenizerMock.Object, _postfixCalculatorMock.Object);
        }

        [Test]
        [TestCaseSource(typeof(XmlCalculatorTestInputs),nameof(XmlCalculatorTestInputs.CalculateInputs))]
        public void ShouldPerformCalculationOperations(List<string> expression,int[] result,List<string> steps)
        {
            var actualSteps = new List<string>();
            _postfixCalculatorMock.Setup(x=>x.CalculateFromPostfix(It.IsAny<List<string>>(),out actualSteps,It.IsAny<bool>()))
                .Callback(new WriteSteps((List<string> postfix,out List<string> stepsList,bool convertSteps)=>stepsList = steps))
                .Returns(result);
            _xmlTokenizerMock.Setup(x => x.ImportXmlFile()).Returns(expression);
            _xmlCalculator.Calculate("","");
            _xmlTokenizerMock.Verify(x=>x.BeginXmlFile(It.IsAny<string>()),Times.Once);
            _xmlTokenizerMock.Verify(x=>x.EndXmlFile(),Times.Once);
            _xmlTokenizerMock.Verify(x=>x.WriteStepXmlFile(It.IsAny<List<string>>()),Times.Exactly(steps.Count+2));
            _postfixCalculatorMock.Verify(x => x.CalculateFromPostfix(It.IsAny<List<string>>(), out actualSteps, It.IsAny<bool>()),Times.Once);
        }
    }
}
