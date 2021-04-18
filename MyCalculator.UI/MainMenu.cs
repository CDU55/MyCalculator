using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCalculator.UI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void AutomaticMode_Click(object sender, EventArgs e)
        {
            FormOpener.Switch(this, new AutomaticMode());
        }

        private void InteractiveMode_Click(object sender, EventArgs e)
        {
            FormOpener.Switch(this, new InteractiveMode());
        }

    }
}
