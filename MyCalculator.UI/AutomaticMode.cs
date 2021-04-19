using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MyCalculator.Parsers;

namespace MyCalculator.UI
{
    public partial class AutomaticMode : Form
    {
        public AutomaticMode()
        {
            InitializeComponent();
            InputFileNameLabel.Text = "";
            OutputFileNameLabel.Text = "";
            MessageLabel.Text = "";

        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            FormOpener.Switch(this, new MainMenu());
        }

        private void InputFileButton_Click(object sender, EventArgs e)
        {
            if (InputFileDialog.ShowDialog() == DialogResult.OK)
            {
                InputFileNameLabel.Text=InputFileDialog.FileName;
            }
        }

        private void OutputFileButton_Click(object sender, EventArgs e)
        {
            if (OutputFileDialog.ShowDialog() == DialogResult.OK)
            {
                OutputFileNameLabel.Text = OutputFileDialog.FileName;
            }
        }

        private void CalculateButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InputFileLabel.Text))
            {
                MessageLabel.Text = "Please select an input file";
                MessageLabel.ForeColor = Color.Red;
            }
            else if (string.IsNullOrEmpty(OutputFileLabel.Text))
            {
                MessageLabel.Text = "Please select an output file";
                MessageLabel.ForeColor=Color.Red;
            }
            else
            {
                try
                {
                    var tokenizer = new XmlTokenizer(InputFileNameLabel.Text, OutputFileNameLabel.Text);
                    var expression = tokenizer.ImportXmlFile();
                    var validator = new TokenValidator();
                    var calculator = new PostfixCalculator(validator, new Calculator(), new TextTokenizer(validator),
                        new PostfixConverter(validator));
                    List<string> steps;
                    var result = calculator.CalculateFromPostfix(expression, out steps, false);
                    tokenizer.BeginXmlFile(string.Join(' ',calculator.FromPostfixToInfix(expression)));
                    tokenizer.WriteStepXmlFile(expression);
                    foreach (var step in steps)
                    {
                        tokenizer.WriteStepXmlFile(step.Split(',').ToList());
                    }
                    tokenizer.WriteStepXmlFile(new List<string>(){result.ConvertToString()});
                    tokenizer.EndXmlFile();
                    Console.WriteLine();
                    MessageLabel.Text = "Calculated";
                    MessageLabel.ForeColor = Color.Black;
                }
                catch (Exception exception)
                {
                    MessageLabel.Text = exception.Message;
                    MessageLabel.ForeColor = Color.Red;
                }
            }
        }
    }
}
