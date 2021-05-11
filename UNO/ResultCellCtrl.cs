using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class ResultCellCtrl : UserControl
    {
        private bool hideResult = true;
        private int result = 0;

        public int Result
        {
            set
            {
                result = value;
                Refresh();
            }
            get
            {
                return result;
            }
        }
        public bool HideResult
        {
            get
            {
                return hideResult;
            }
            set
            {
                hideResult = value;
                Refresh();
            }
        }

        public ResultCellCtrl()
        {
            InitializeComponent();
        }

        private void ResultCellCtrl_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            if (hideResult)
            {
                gr.DrawString("?", UNOFonts.WriteFont12, new SolidBrush(UNOOneGame.PenColor), this.ClientRectangle, UNOOneGame.sf);
            }
            else
            {
                gr.DrawString(result.ToString(), UNOFonts.WriteFont12, new SolidBrush(UNOOneGame.PenColor), this.ClientRectangle, UNOOneGame.sf);
            }
        }
    }
}
