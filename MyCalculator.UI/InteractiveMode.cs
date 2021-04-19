using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyCalculator.Parsers;

namespace MyCalculator.UI
{
    public partial class InteractiveMode : Form
    {
        private readonly PostfixCalculator _calculator;
        public InteractiveMode()
        {
            InitializeComponent();
            var validator = new TokenValidator();
            _calculator = new PostfixCalculator(new TokenValidator(), new Calculator(), new TextTokenizer(validator),
                new PostfixConverter(validator));
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            FormOpener.Switch(this, new MainMenu());
        }

        private void Calculate_Click(object sender, EventArgs e)
        {
            var expression = ExpressionInput.Text;
            try
            {
                List<string> steps;
                var result = _calculator.Calculate(expression,out steps);
                OutputBox.Text = "";
                foreach (var step in steps)
                {
                    OutputBox.Text += step;
                    OutputBox.Text += "\n";
                }
                OutputBox.Text += result.ConvertToString();
                OutputBox.ForeColor = Color.Black;
            }
            catch (Exception exception)
            {
                OutputBox.Text = exception.ToString();
                OutputBox.ForeColor = Color.Red;
            }
        }

    }
}
