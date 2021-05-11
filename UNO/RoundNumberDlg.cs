using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class RoundNumberDlg : Form
    {
        public int Maximum
        {
            set
            {
                numericUpDown1.Maximum = value + 1;
            }
        }
        public int Value
        {
            set
            {
                numericUpDown1.Value = value;
            }
            get
            {
                return (int)(numericUpDown1.Value - 1);
            }
        }

        public RoundNumberDlg()
        {
            InitializeComponent();
        }
    }
}