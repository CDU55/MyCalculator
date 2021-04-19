using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyCalculator.Parsers
{
    public class XmlTokenizer : ITokenizer
    {

        public List<string> Tokenize(string input)
        {
            throw new NotImplementedException();
        }

        public XmlTokenizer(string inputPath, string outputPath)
        {
            importPath = inputPath;
            exportPath = outputPath;
        }

        static string exportPath;
        static string importPath;

        public void WriteStepXmlFile(List<String> stepList)
        {
            List<string> operators = new List<string>() { "+", "-", "/", "*", "pow", "sqrt" };
            Stack<string> numbers = new Stack<string>();
            string firstNumber, secondNumber;
            foreach (string step in stepList)
            {
                if (operators.Any(step.Contains))
                {
                    secondNumber = numbers.Pop();
                    firstNumber = numbers.Pop();
                    switch (step)
                    {
                        case "+":
                            numbers.Push("add@" + firstNumber + "@" + secondNumber + "@/add");
                            //Console.WriteLine(firstNumber + " + " + secondNumber);
                            break;
                        case "-":
                            numbers.Push("substraction@" + firstNumber + "@" + secondNumber + "@/substraction");
                            //Console.WriteLine(step);
                            break;
                        case "/":
                            numbers.Push("div@" + firstNumber + "@" + secondNumber + "@/div");
                            //Console.WriteLine(step);
                            break;
                        case "*":
                            numbers.Push("multiply@" + firstNumber + "@" + secondNumber + "@/multiply");
                            //Console.WriteLine(step);
                            break;
                        case "pow":
                            numbers.Push("^@" + firstNumber + "@" + secondNumber + "@/pow");
                            //Console.WriteLine(step);
                            break;
                        case "sqrt":
                            numbers.Push("#@" + firstNumber + "@" + secondNumber + "@/sqrt");
                            //Console.WriteLine(step);
                            break;
                        default:
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
            string identation = "  ";

            using (StreamWriter sw = File.AppendText(exportPath))
            {

                sw.WriteLine("          <step>");
                foreach (string token in tokensList)
                {
                    if (int.TryParse(token, out _))
                    {
                        identation = new string('\t', noIdentation + 1);
                        sw.Write(identation + "<number>" + token + "</number>");
                        sw.WriteLine();
                    }
                    else
                    {

                        //Console.WriteLine("AICI ESTE " + i + " cu " + noIdentation);
                        if (token.Contains("/"))
                        {
                            identation = new string('\t', noIdentation);
                            sw.Write(identation + "<" + token + ">");
                            sw.WriteLine();
                            noIdentation--;

                        }
                        else
                        {
                            noIdentation++;
                            identation = new string('\t', noIdentation);
                            sw.Write(identation + "<" + token + ">");
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
            File.WriteAllText(exportPath, string.Empty);
            using (StreamWriter sw = File.CreateText(exportPath))
            {
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"UTF - 8\"?>");
                sw.WriteLine("<exercise>");
                sw.WriteLine("  <formula>" + formula + "</formula>");
                sw.WriteLine("      <steps>");
            }
        }

        public void EndXmlFile()
        {
            using StreamWriter sw = File.AppendText(exportPath);
            sw.WriteLine("      </steps>");
            sw.WriteLine("</exercise>");
        }

        public List<string> ImportXmlFile()
        {
            List<string>formula=new List<string>();
            Stack<string> numbers = new Stack<string>();
            Stack<string> operators = new Stack<string>();
            Dictionary<string, string> operatorsDict = new Dictionary<string, string>() { { "add", "+" }, { "substraction", "-" }, { "div", "/" }, { "multiply", "*" }, { "pow", "pow" }, { "sqrt", "sqrt" } };
            foreach (string line in File.ReadLines(importPath))
                {
                    string newLine = line.Trim().Replace("<", "").Replace(">", "");
                    if (operatorsDict.Keys.Contains(newLine) && !newLine.Contains("/"))
                    {
                        ;
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
                formula.Add(numbers.Pop());
            }

            formula.Reverse();
            return formula;
        }

    }
}
