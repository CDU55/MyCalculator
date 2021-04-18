using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyCalculator.UI
{
    public partial class AutomaticMode : Form
    {
        public AutomaticMode()
        {
            InitializeComponent();
            InputFileLabel.Text = "";
            OutputFileLabel.Text = "";
            MessageLabel.Text = "";
        }
        private void BackButton_Click(object sender, EventArgs e)
        {
            FormOpener.Switch(this, new MainMenu());
        }

        private void InputFileButton_Click(object sender, EventArgs e)
        {
            InputFileDialog.Filter = "XML files (*.xml)";
            if (InputFileDialog.ShowDialog() == DialogResult.OK)
            {
                InputFileLabel.Text=InputFileDialog.FileName;
            }
        }

        private void OutputFileButton_Click(object sender, EventArgs e)
        {
            OutputFileDialog.Filter = "XML files (*.xml)";
            if (OutputFileDialog.ShowDialog() == DialogResult.OK)
            {
                InputFileLabel.Text = OutputFileDialog.FileName;
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
                MessageLabel.Text = "Calculated";
                MessageLabel.ForeColor=Color.Black;
            }
        }
    }
}
