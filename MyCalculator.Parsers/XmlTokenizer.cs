using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using MyCalculator.Parsers.Interfaces;

namespace MyCalculator.Parsers
{
    public class XmlTokenizer : IXmlTokenizer
    {
        public string _exportPath { get; set; }
        public string _importPath { get; set; }

        public XmlTokenizer()
        {

        }
        public XmlTokenizer(string inputFile, string outputFile)
        {
            Contract.Assert(inputFile != null && outputFile != null);
            _importPath = inputFile;
            _exportPath = outputFile;
        }
        public void WriteStepXmlFile(List<String> stepList)
        {
            Contract.Assert(stepList.Count != 0);
            List<string> operators = new List<string>() {"+", "-", "/", "*", "pow", "sqrt"};
            Stack<string> numbers = new Stack<string>();
            foreach (string step in stepList)
            {
                if (operators.Any(step.Contains))
                {
                    Contract.Assert(numbers.Count > 0);
                    var secondNumber = numbers.Pop();
                    Contract.Assert(numbers.Count > 0);
                    var firstNumber = numbers.Pop();
                    switch (step)
                    {
                        case "+":
                            numbers.Push("add@" + firstNumber + "@" + secondNumber + "@/add");
                            break;
                        case "-":
                            numbers.Push("substraction@" + firstNumber + "@" + secondNumber + "@/substraction");
                            break;
                        case "/":
                            numbers.Push("div@" + firstNumber + "@" + secondNumber + "@/div");
                            break;
                        case "*":
                            numbers.Push("multiply@" + firstNumber + "@" + secondNumber + "@/multiply");
                            break;
                        case "pow":
                            numbers.Push("^@" + firstNumber + "@" + secondNumber + "@/pow");
                            break;
                        case "sqrt":
                            numbers.Push("#@" + firstNumber + "@" + secondNumber + "@/sqrt");
                            break;
                    }
                }
                else
                {
                    numbers.Push(step);
                }
            }

            string[] tokensList = numbers.Pop().Split("@");
            int noIdentation = 1;
            using (StreamWriter sw = File.AppendText(_exportPath))
            {
                sw.WriteLine("          <step>");
                foreach (string token in tokensList)
                {
                    var indentation = "  ";
                    if (int.TryParse(token, out _))
                    {
                        indentation = new string('\t', noIdentation + 1);
                        sw.Write(indentation + "<number>" + token + "</number>");
                        sw.WriteLine();
                    }
                    else
                    {
                        if (token.Contains("/"))
                        {
                            indentation = new string('\t', noIdentation);
                            sw.Write(indentation + "<" + token + ">");
                            sw.WriteLine();
                            noIdentation--;
                        }
                        else
                        {
                            noIdentation++;
                            indentation = new string('\t', noIdentation);
                            sw.Write(indentation + "<" + token + ">");
                            sw.WriteLine();
                        }
                    }
                }

                sw.WriteLine("          </step>");
                sw.WriteLine();
            }
        }

        public void BeginXmlFile(string formula)
        {
            Contract.Assert(formula != null);
            File.WriteAllText(_exportPath, string.Empty);
            using var sw = File.CreateText(_exportPath);
            sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>");
            sw.WriteLine("<exercise>");
            sw.WriteLine("  <formula>" + formula + "</formula>");
            sw.WriteLine("      <steps>");
        }

        public void EndXmlFile()
        {
            using var sw = File.AppendText(_exportPath);
            {
                sw.WriteLine("      </steps>");
                sw.WriteLine("</exercise>");
            }
        }

        public List<string> ImportXmlFile()
        {
            var formula = new List<string>();
            var numbers = new Stack<string>();
            var operators = new Stack<string>();
            var operatorsDict = new Dictionary<string, string>()
            {
                {"add", "+"}, {"substraction", "-"}, {"div", "/"}, {"multiply", "*"}, {"pow", "pow"}, {"sqrt", "sqrt"}
            };
            Contract.Assert(File.Exists(_importPath));
            foreach (var line in File.ReadLines(_importPath))
            {
                var newLine = line.Trim().Replace("<", "").Replace(">", "");
                if (operatorsDict.Keys.Contains(newLine) && !newLine.Contains("/"))
                {
                    operators.Push(operatorsDict[newLine]);
                }
                else if (newLine.StartsWith("maxSize") && newLine.EndsWith("/maxSize"))
                {
                    PostfixCalculator.SetMaxArraySize(newLine.Replace("maxSize", "").Replace("/", ""));
                }
                else
                {
                    if (newLine.StartsWith("number") && newLine.EndsWith("/number"))
                    {
                        numbers.Push(newLine.Replace("number", "").Replace("/", ""));
                    }
                    else if (operatorsDict.Keys.Contains(newLine.Replace("/", "")) && newLine.Contains("/"))
                    {
                        numbers.Push(operators.Pop());
                    }
                }
            }

            while (numbers.Count != 0)
            {
                Contract.Assert(numbers.Count>0);
                formula.Add(numbers.Pop());
            }

            formula.Reverse();
            Contract.Assert(formula.Count != 0);
            return formula;
        }
    }
}