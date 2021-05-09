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
        private readonly XmlCalculator _calculator;

        public AutomaticMode()
        {
            InitializeComponent();
            InputFileNameLabel.Text = "";
            OutputFileNameLabel.Text = "";
            MessageLabel.Text = "";
            var validator = new TokenValidator();
            _calculator = new XmlCalculator(new XmlTokenizer(), new PostfixCalculator(new TokenValidator(),
                new Calculator(), new TextTokenizer(validator),
                new PostfixConverter(validator)));

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
                    _calculator.Calculate(InputFileNameLabel.Text, OutputFileNameLabel.Text);
                    MessageLabel.Text = @"Calculated";
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
