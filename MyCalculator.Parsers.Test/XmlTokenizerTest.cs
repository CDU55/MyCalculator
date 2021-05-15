using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MyCalculator.Parsers.Test
{
    [TestFixture]
    public class XmlTokenizerTest
    {
        private XmlTokenizer _xmlTokenizer;
        private XmlTokenizer _xmlTokenizerSecond;
        private string expectedEndPath = @"..\..\..\ExpectedInput\expected_end.txt";
        private string expectedBeginPath = @"..\..\..\ExpectedInput\expected_begin.txt";
        private string expectedStep = @"..\..\..\ExpectedInput\expected_step.txt";
        private string expectedImport = @"..\..\..\ExpectedInput\expected_import.txt";
        private string expectedOutput = "";
        private string generatedOutput = "";

        [SetUp]
        public void SetUp()
        {
            string outputPath = @"..\..\..\ExpectedInput\output.txt";
            string inputPath = @"..\..\..\ExpectedInput\input.txt";
            _xmlTokenizer = new XmlTokenizer(inputPath, outputPath);
            _xmlTokenizerSecond = new XmlTokenizer();
        }

        [Test]
        public void ShouldReturnTheEndOfXmlFile()
        {
            File.WriteAllText(_xmlTokenizer._exportPath, string.Empty);
            _xmlTokenizer.EndXmlFile();

            using (var sr = new StreamReader(expectedEndPath))
            {
                expectedOutput = sr.ReadToEnd();
            }

            using (var sr = new StreamReader(_xmlTokenizer._exportPath))
            {
                generatedOutput = sr.ReadToEnd();
            }

            CollectionAssert.AreEqual(expectedOutput, generatedOutput);
        }

        [Test]
        public void ShouldReturnTheBeginXmlFile()
        {
            File.WriteAllText(_xmlTokenizer._exportPath, string.Empty);
            _xmlTokenizer.BeginXmlFile("Formula");

            using (var sr = new StreamReader(expectedBeginPath))
            {
                expectedOutput = sr.ReadToEnd();
            }

            using (var sr = new StreamReader(_xmlTokenizer._exportPath))
            {
                generatedOutput = sr.ReadToEnd();
            }

            CollectionAssert.AreEqual(expectedOutput, generatedOutput);
        }

        [Test]
        public void ShouldReturnListOfSteps()
        {
            List<string> generatedOutput = _xmlTokenizer.ImportXmlFile();
            List<string> expectedOutput = new List<string>();
            string line;

            StreamReader file = new StreamReader(expectedImport);
            while ((line = file.ReadLine()) != null)
            {
                System.Console.WriteLine(line);
                expectedOutput.Add(line);
            }

            CollectionAssert.AreEqual(generatedOutput, expectedOutput);
        }

        [Test]
        public void ShouldGenerateStepsBasedOnList()
        {
            List<string> stepList = _xmlTokenizer.ImportXmlFile();
            File.WriteAllText(_xmlTokenizer._exportPath, string.Empty);
            _xmlTokenizer.WriteStepXmlFile(stepList);

            using (var sr = new StreamReader(expectedStep))
            {
                expectedOutput = sr.ReadToEnd();
            }

            using (var sr = new StreamReader(_xmlTokenizer._exportPath))
            {
                generatedOutput = sr.ReadToEnd();
            }

            CollectionAssert.AreEqual(generatedOutput, expectedOutput);
        }

    }
}