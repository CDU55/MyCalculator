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
        private readonly ITokenizer _tokenizer;
        private readonly IPostfixConverter _converter;
        public InteractiveMode()
        {
            InitializeComponent();
            _tokenizer = new TextTokenizer(new TokenValidator());
            _converter = new PostfixConverter(new TokenValidator());
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
                var tokens = _tokenizer.Tokenize(expression);
                var postfix = _converter.ConvertToPostfix(tokens);
                OutputBox.Text = string.Join(' ', postfix);
                OutputBox.ForeColor = Color.Black;
            }
            catch (Exception exception)
            {
                OutputBox.Text = exception.Message;
                OutputBox.ForeColor = Color.Red;
            }
        }

    }
}
