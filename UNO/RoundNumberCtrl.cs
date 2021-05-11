using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class RoundNumberCtrl : UserControl
    {
        private int number = 0;
        public int Number
        {
            set
            {
                number = value;
            }
            get
            {
                return number;
            }
        }

        public RoundNumberCtrl()
        {
            InitializeComponent();
        }

        private void RoundNumberCtrl_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawString(number.ToString(), UNOFonts.WriteFont12, new SolidBrush(UNOOneGame.PenColor),
                this.ClientRectangle, UNOOneGame.sf);
        }
    }
}
