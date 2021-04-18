using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyCalculator.UI
{
    public class FormOpener
    {
        public static void Switch(Form caller, Form toOpen)
        {
            caller.Hide();
            toOpen.ShowDialog();
            caller.Close();
        }
        public static void Open(Form toOpen)
        {
            toOpen.Show();
        }
    }
}
