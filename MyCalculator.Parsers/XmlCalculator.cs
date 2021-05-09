using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCalculator.Parsers.Interfaces;

namespace MyCalculator.Parsers
{
    public class XmlCalculator
    {
        private readonly IPostfixCalculator _postfixCalculator;
        private readonly IXmlTokenizer _xmlTokenizer;

        public XmlCalculator(IXmlTokenizer xmlTokenizer,IPostfixCalculator postfixCalculator)
        {
            _xmlTokenizer = xmlTokenizer;
            _postfixCalculator = postfixCalculator;
        }

        public void Calculate(string inputFile,string outputFile)
        {
            _xmlTokenizer._importPath = inputFile;
            _xmlTokenizer._exportPath = outputFile;
            var expression = _xmlTokenizer.ImportXmlFile(); 
            List<string> steps;
            var result = _postfixCalculator.CalculateFromPostfix(expression, out steps, false);
            _xmlTokenizer.BeginXmlFile(string.Join(' ', _postfixCalculator.FromPostfixToInfix(expression)));
            _xmlTokenizer.WriteStepXmlFile(expression);
            foreach (var step in steps)
            {
                _xmlTokenizer.WriteStepXmlFile(step.Split(',').ToList());
            }
            _xmlTokenizer.WriteStepXmlFile(new List<string>() {
                result.ConvertToString()
            });
            _xmlTokenizer.EndXmlFile();
    }
}
}
