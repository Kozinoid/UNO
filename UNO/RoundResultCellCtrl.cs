using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class RoundResultCellCtrl : UserControl
    {
        public event EventHandler ResultValueChanged = null;
        public event EventHandler WinStateChanged = null;

        private int result = 0;
        private Bitmap winBMP = null;
        private bool winner = false;

        public int Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                Refresh();
            }
        }
        public bool Winner
        {
            set
            {
                if (result != 0)
                {
                    winner = false;
                }
                else
                {
                    winner = value;
                }
                Refresh();
            }
            get
            {
                return winner;
            }
        }

        public RoundResultCellCtrl()
        {
            InitializeComponent();

            winBMP = Properties.Resources.WinnerMark;
            winBMP.MakeTransparent();
            valueTextBox.LostFocus += new EventHandler(valueTextBox_LostFocus);
        }

        void valueTextBox_LostFocus(object sender, EventArgs e)
        {
            OnValueChanged();
        }

        private void valueTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnValueChanged();
            }
        }

        private void OnValueChanged()
        {
            valueTextBox.Visible = false;
            try
            {
                result = int.Parse(valueTextBox.Text);
            }
            catch
            {
                result = 0;
                valueTextBox.Text = "0";
                MessageBox.Show("Для ввода можно ипользовать только цифры!");
            }
            if (ResultValueChanged != null) ResultValueChanged(this, null);
        }

        private void RoundResultCellCtrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                valueTextBox.Text = result.ToString();
                valueTextBox.Visible = true;
                valueTextBox.SelectAll();
                valueTextBox.Focus();
            }
        }

        private void RoundResultCellCtrl_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            if (winner)
            {
                gr.DrawImage(winBMP, new Point(0, 0));
            }

            gr.DrawString(result.ToString(), UNOFonts.WriteFont12, new SolidBrush(UNOOneGame.PenColor), this.ClientRectangle, UNOOneGame.sf);
        }

        private void RoundResultCellCtrl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (result == 0)
                {
                    winnerToolStripMenuItem.Checked = winner;
                    contextMenuStrip1.Show(Control.MousePosition.X, Control.MousePosition.Y);
                }
            }
        }

        private void winnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            winnerToolStripMenuItem.Checked = !winnerToolStripMenuItem.Checked;
            winner = winnerToolStripMenuItem.Checked;
            Refresh();
            if (WinStateChanged != null) WinStateChanged(this, null);
        }
    }
}
