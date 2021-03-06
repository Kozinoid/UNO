using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class NewUNODlg : Form
    {
        const int numY = 90;
        const int numH1 = 40;
        const int numH2 = 40;
        const int playerH = 40;

        private int playersCount = 4;
        public const int minPlayers = 2;
        public const int maxPlayers = 8;
        private StringFormat sf = new StringFormat();
        private Color numColor = Color.Blue;
        private UNOMenu2ColumnsItem[] playerPosData = new UNOMenu2ColumnsItem[maxPlayers];
        private TextBox textBox = null;
        private int selectedName = -1;
        private UNOMenuItem buttonOk = null;
        private Bitmap labBMP = null;

        public int PlayerCount
        {
            get
            {
                return playersCount;
            }
        }
        public string[] PlayersNames
        {
            get
            {
                string[] names = new string[maxPlayers];
                for (int i = 0; i < maxPlayers; i++)
                {
                    names[i] = playerPosData[i].ValueField;
                }
                return names;
            }
        }

        public NewUNODlg()
        {
            InitializeComponent();

            textBox = new TextBox();
            textBox.Font = new Font("Times New Roman", 18f);
            textBox.Location = new Point(0, 0);
            textBox.Size = new Size(this.ClientRectangle.Width / 2, playerH);
            textBox.Visible = false;
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            textBox.LostFocus += new EventHandler(textBox_LostFocus);
            this.Controls.Add(textBox);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            for (int i = 0; i < maxPlayers; i++)
            {
                playerPosData[i] = 
                    new UNOMenu2ColumnsItem(string.Format("Игрок №{0}:", i + 1), "Player" + (i + 1).ToString(), new Rectangle(0, numY + numH1 + numH2 + playerH * i, this.ClientRectangle.Width, playerH), Color.Blue);
            }

            buttonOk = new UNOMenuItem("Ok", new Rectangle(this.ClientRectangle.Width / 2 - 40, this.ClientRectangle.Height - 40, 80, 40), Color.Blue);

            labBMP = Properties.Resources.New_Game;
            labBMP.MakeTransparent();
        }

        void textBox_LostFocus(object sender, EventArgs e)
        {
            EnterNewName();
        }

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EnterNewName();
            }
            OnKeyUp(e);
        }

        private void EnterNewName()
        {
            textBox.Visible = false;
            playerPosData[selectedName].ValueField = textBox.Text;
            Refresh();
        }

        private void NewUNODlg_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            gr.DrawImage(labBMP, new Point(0, 0));

            Rectangle numRect1 = new Rectangle(0, numY, this.ClientRectangle.Width, numH1);
            Rectangle numRect2 = new Rectangle(0, numY + numH1, this.ClientRectangle.Width, numH2);
            Rectangle numRect1s = new Rectangle(0 + 1, numY + 1, this.ClientRectangle.Width, numH1);
            Rectangle numRect2s = new Rectangle(0 + 1, numY + numH1 + 1, this.ClientRectangle.Width, numH2);
            gr.DrawString(string.Format("Число игроков (2-8): {0}", playersCount), UNOFonts.WriteFont18, new SolidBrush(Color.Gray),
                numRect1s, sf);
            gr.DrawString(string.Format("(↑ - увеличить, ↓ - уменьшить.)", playersCount), UNOFonts.WriteFont12, new SolidBrush(Color.Gray),
                numRect2s, sf);
            gr.DrawString(string.Format("Число игроков (2-8): {0}", playersCount), UNOFonts.WriteFont18, new SolidBrush(numColor),
                numRect1, sf);
            gr.DrawString(string.Format("(↑ - увеличить, ↓ - уменьшить.)", playersCount), UNOFonts.WriteFont12, new SolidBrush(numColor),
                numRect2, sf);

            for (int i = 0; i < playersCount; i++)
            {
                Rectangle rect1 = playerPosData[i].NameRectArea;
                Rectangle rect2 = playerPosData[i].NameShadowRectArea;
                gr.DrawString(playerPosData[i].NameField, UNOFonts.WriteFont18, new SolidBrush(Color.Gray),
                    rect2, sf);
                gr.DrawString(playerPosData[i].NameField, UNOFonts.WriteFont18, new SolidBrush(numColor),
                    rect1, sf);
                Rectangle rect3 = playerPosData[i].ValueRectArea;
                Rectangle rect4 = playerPosData[i].ValueShadowRectArea;
                gr.DrawString(playerPosData[i].ValueField, UNOFonts.WriteFont18, new SolidBrush(Color.Gray),
                    rect4, sf);
                gr.DrawString(playerPosData[i].ValueField, UNOFonts.WriteFont18, new SolidBrush(numColor),
                    rect3, sf);
            }

            gr.DrawString(buttonOk.NameField, UNOFonts.WriteFont18, new SolidBrush(Color.Gray),
                    buttonOk.AllShadowRectArea, sf);
            gr.DrawString(buttonOk.NameField, UNOFonts.WriteFont18, new SolidBrush(buttonOk.NameColor),
                buttonOk.AllRectArea, sf);
        }

        private void NewUNODlg_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    playersCount++;
                    if (playersCount > maxPlayers)
                    {
                        playersCount = maxPlayers;
                    }
                    else
                    {
                        Refresh();
                    }
                    break;
                case Keys.Down:
                    playersCount--;
                    if (playersCount < minPlayers)
                    {
                        playersCount = minPlayers;
                    }
                    else
                    {
                        Refresh();
                    }
                    break;
            }
        }

        private void NewUNODlg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < playersCount; i++)
                {
                    if (playerPosData[i].AllRectArea.Contains(e.Location))
                    {
                        selectedName = i;
                        textBox.Text = playerPosData[i].ValueField;
                        textBox.Location = playerPosData[i].ValueRectArea.Location;
                        textBox.Size = playerPosData[i].ValueRectArea.Size;
                        textBox.Visible = true;
                        textBox.Focus();
                        textBox.SelectAll();
                    }
                }
            }
        }

        private void NewUNODlg_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (buttonOk.AllRectArea.Contains(e.Location))
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}