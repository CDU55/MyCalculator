using System;
using System.Collections.Generic;
using System.Text;

namespace MyCalculator.Parsers.Interfaces
{
    public interface IXmlTokenizer
    {
        void WriteStepXmlFile(List<String> stepList);
        void BeginXmlFile(string formula);
        void EndXmlFile();
        List<string> ImportXmlFile();
        public string _exportPath { get; set; }
        public string _importPath { get; set; }


    }
}
