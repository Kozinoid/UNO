using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace UNO
{
    public partial class GameTittlesDlg : Form
    {
        const int spaceH = 80;
        const int nameH = 40;
        const int dateH = 40;

        private UNOMenu2ColumnsItem stageName = null;
        private UNOMenu2ColumnsItem stageDate = null;
        private UNOMenuItem buttonOk = null;
        private StringFormat sf = new StringFormat();
        private TextBox textBox = null;
        private Bitmap labBMP = null;

        public string StageName
        {
            get
            {
                return stageName.ValueField;
            }
            set
            {
                stageName.ValueField = value;
            }
        }
        public string StageDate
        {
            get
            {
                return stageDate.ValueField;
            }
            set
            {
                stageDate.ValueField = value;
            }
        }

        public GameTittlesDlg()
        {
            InitializeComponent();

            stageName = new UNOMenu2ColumnsItem("Название этапа:", "Новый UNO-этап", new Rectangle(0, spaceH, this.ClientRectangle.Width, nameH), Color.Blue);
            stageDate = new UNOMenu2ColumnsItem("Дата:", GetDateString(DateTime.Now), new Rectangle(0, spaceH + nameH, this.ClientRectangle.Width, dateH), Color.Blue);

            textBox = new TextBox();
            textBox.Font = new Font("Times New Roman", 16f);
            textBox.Location = stageName.ValueRectArea.Location;
            textBox.Size = stageName.ValueRectArea.Size;
            textBox.Visible = false;
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            textBox.LostFocus += new EventHandler(textBox_LostFocus);
            this.Controls.Add(textBox);

            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            buttonOk = new UNOMenuItem("Ok", new Rectangle(this.ClientRectangle.Width / 2 - 40, this.ClientRectangle.Height - 40, 80, 40), Color.Blue);

            labBMP = Properties.Resources.New_Stage;
            labBMP.MakeTransparent(Color.White);
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
            stageName.ValueField = textBox.Text;
            Refresh();
        }

        public static string GetDateString(DateTime date)
        {
            string str = date.ToString("dd.MM.yy");
            return str;
        }

        private void GameTittlesDlg_Paint(object sender, PaintEventArgs e)
        {
            Graphics gr = e.Graphics;

            gr.DrawImage(labBMP, new Point(0, 0));

            gr.DrawString(stageName.NameField, UNOFonts.WriteFont16, new SolidBrush(Color.Gray), stageName.NameShadowRectArea, sf);
            gr.DrawString(stageName.NameField, UNOFonts.WriteFont16, new SolidBrush(stageName.NameColor), stageName.NameRectArea, sf);
            gr.DrawString(stageName.ValueField, UNOFonts.WriteFont16, new SolidBrush(Color.Gray), stageName.ValueShadowRectArea, sf);
            gr.DrawString(stageName.ValueField, UNOFonts.WriteFont16, new SolidBrush(stageName.NameColor), stageName.ValueRectArea, sf);

            gr.DrawString(stageDate.NameField, UNOFonts.WriteFont16, new SolidBrush(Color.Gray), stageDate.NameShadowRectArea, sf);
            gr.DrawString(stageDate.NameField, UNOFonts.WriteFont16, new SolidBrush(stageDate.NameColor), stageDate.NameRectArea, sf);
            gr.DrawString(stageDate.ValueField, UNOFonts.WriteFont16, new SolidBrush(Color.Gray), stageDate.ValueShadowRectArea, sf);
            gr.DrawString(stageDate.ValueField, UNOFonts.WriteFont16, new SolidBrush(stageDate.NameColor), stageDate.ValueRectArea, sf);

            gr.DrawString(buttonOk.NameField, UNOFonts.WriteFont18, new SolidBrush(Color.Gray),
                    buttonOk.AllShadowRectArea, sf);
            gr.DrawString(buttonOk.NameField, UNOFonts.WriteFont18, new SolidBrush(buttonOk.NameColor),
                buttonOk.AllRectArea, sf);
        }

        private void GameTittlesDlg_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (stageName.AllRectArea.Contains(e.Location))
                {
                    textBox.Text = stageName.ValueField;
                    textBox.Location = stageName.ValueRectArea.Location;
                    textBox.Size = stageName.ValueRectArea.Size;
                    textBox.Visible = true;
                    textBox.Focus();
                    textBox.SelectAll();
                }
                if (stageDate.AllRectArea.Contains(e.Location))
                {
                    CalendarDlg cd = new CalendarDlg();
                    if (cd.ShowDialog() == DialogResult.OK)
                    {
                        stageDate.ValueField = GetDateString(cd.PickedDate);
                        Refresh();
                    }
                }
            }
        }

        private void GameTittlesDlg_MouseClick(object sender, MouseEventArgs e)
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