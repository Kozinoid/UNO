using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class PlayerNameHeaderCtrl : UserControl
    {
        public event EventHandler PlayerNameChanged = null;

        public string PlayerName
        {
            get
            {
                return nameTextBox.Text;
            }
            set
            {
                nameTextBox.Text = value;
                Refresh();
            }
        }

        public PlayerNameHeaderCtrl()
        {
            InitializeComponent();

            nameTextBox.LostFocus += new EventHandler(nameTextBox_LostFocus);
        }

        void nameTextBox_LostFocus(object sender, EventArgs e)
        {
            OnPlayerNameChanged();
        }

        private void nameTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OnPlayerNameChanged();
            }
        }

        private void OnPlayerNameChanged()
        {
            nameTextBox.Visible = false;
            if (PlayerNameChanged != null) PlayerNameChanged(this, null);
        }

        private void PlayerNameHeaderCtrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                nameTextBox.Visible = true;
                nameTextBox.SelectAll();
                nameTextBox.Focus();
            }
        }

        private void PlayerNameHeaderCtrl_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;
            gr.DrawString(nameTextBox.Text, UNOFonts.WriteFont12, new SolidBrush(UNOOneGame.PenColor), this.ClientRectangle, UNOOneGame.sf);
        }
    }
}
